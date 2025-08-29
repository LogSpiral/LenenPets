using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.Utilities;
using TouhouPets;
using TouhouPets.Common.ModSupports.ModPetRegisterSystem;
using TouhouPets.Content.Buffs;
using TouhouPets.Content.Projectiles.Pets;

namespace LenenPets.Content.Pets.MyPet;

public class MyPetBuff : BasicPetBuff
{
    public override bool IsLoadingEnabled(Mod mod) => false;
    public override string Texture => $"Terraria/Images/Item_{ItemID.FragmentVortex}";
    public override int PetType => ProjectileType<MyPet>(); // 设为与之相应的宠物
    public override bool LightPet => true; // 标记为照明宠物
}
public class MyPetItem : ModItem
{
    public override bool IsLoadingEnabled(Mod mod) => false;
    public override string Texture => $"Terraria/Images/Item_{ItemID.FragmentVortex}";

    public override void SetDefaults()
    {
        Item.DefaultToVanitypet(ProjectileType<MyPet>(), BuffType<MyPetBuff>());    // 将物品与宠物和Buff绑定
        Item.DefaultToVanitypetExtra(26, 34);                                       // 设置物品宽高
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (!player.HasBuff(Item.buffType))         // 发射宠物的同时添加buff
            player.AddBuff(Item.buffType, 2);
        return false;                               // Buff的更新函数负责生成宠物，所以这里是false
    }
}
public class MyPet : BasicTouhouPet
{
    public override bool IsLoadingEnabled(Mod mod) => false;

    const int FrameCount = 4;
    #region 初始化
    public override void PetStaticDefaults()
    {
        Main.projPet[Type] = true;                      // 标记为宠物
        Main.projFrames[Type] = FrameCount;             // 设置最大帧数
        ProjectileID.Sets.TrailCacheLength[Type] = 4;   // 记录四帧
        ProjectileID.Sets.LightPet[Type] = true;        // 标记为照明宠物
        base.PetStaticDefaults();
    }

    public override void PetDefaults()
    {
        Projectile.width = Projectile.height = 32;  // 设置宠物宽高
        Projectile.tileCollide = true;              // 添加物块判定
    }
    #endregion

    #region 视觉效果类
    public override void VisualEffectForPreview()
    {
        // 每10帧切换一次帧图
        Projectile.frameCounter++;
        if (Projectile.frameCounter > 10) 
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;
            Projectile.frame %= FrameCount;
        }

        // 根据状态记录数据
        Vector2 offset = default;
        switch (CurrentState) 
        {
            case State.Rolling:
                Projectile.rotation += 0.2f;
                break;
            case State.Shaking:
                offset = Main.rand.NextVector2Unit() * Main.rand.NextFloat(8);
                break;
        }
        for (int n = 3; n > 0; n--)
        {
            Projectile.oldPos[n] = Projectile.oldPos[n - 1];
            Projectile.oldRot[n] = Projectile.oldRot[n - 1];
        }
        Projectile.oldPos[0] = Projectile.Center + offset;
        Projectile.oldRot[0] = Projectile.rotation;
        
    }
    public override bool DrawPetSelf(ref Color lightColor)
    {
        // 绘制，按你喜好来
        Texture2D texture = TextureAssets.Projectile[Type].Value;
        for (int n = 0; n < 4; n++)
        {
            Main.EntitySpriteDraw(
                texture,
                Projectile.oldPos[n] - Main.screenPosition,
                texture.Frame(1, FrameCount, 0, (Projectile.frame + n) % FrameCount),
                Color.White * (1 - n * .25f),
                Projectile.oldRot[n],
                new Vector2(16),
                1,
                0,
                0);
        }
        return false;
    }
    #endregion

    #region 宠物更新
    enum State
    {
        Idle,    // 闲置
        Rolling, // 滚动
        Shaking  // 抖动
    }
    // 给PetState套层壳
    State CurrentState
    {
        get => (State)PetState;
        set => PetState = (int)value;
    }
    int Timer
    {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }
    // 检测并更新宠物是否应该继续存在
    bool CheckActive()
    {
        Player player = Owner;
        Projectile.timeLeft = 2;

        bool noActiveBuff = !player.HasBuff(BuffType<MyPetBuff>());
        bool shouldInactiveNormally = noActiveBuff;

        if (shouldInactiveNormally || player.dead)
        {
            Projectile.velocity *= 0;
            Projectile.frame = 0;
            Projectile.Opacity -= 0.009f;
            if (Projectile.Opacity <= 0)
            {
                Projectile.active = false;
                Projectile.netUpdate = true;
            }
            return false;
        }
        return true;

    }
    // 控制宠物移动
    // 这里我让它绕着鼠标转圈
    private void ControlMovement()
    {
        Projectile.tileCollide = false;

        ChangeDir();

        if (!Owner.dead)
            MoveToPoint(
                (Main.GlobalTimeWrappedHourly * 4).ToRotationVector2() * 64,
                24,
                Main.MouseWorld);
    }
    // 更新状态
    private void UpdateState()
    {
        // 计时器自增
        Timer++;
        switch (CurrentState)
        {
            case State.Idle:
                {
                    if (Timer >= 600)
                    {
                        // 在滚动和抖动间二选一
                        CurrentState = Main.rand.Next([State.Rolling, State.Shaking]);
                        Timer = 0;
                        Projectile.netUpdate = true;
                    }
                    break;
                }
            case State.Rolling: 
                {
                    if (Timer >= 450)
                    {
                        // 在所有状态中进行加权随机
                        var rand = new WeightedRandom<State>();
                        rand.Add(State.Idle, 2);
                        rand.Add(State.Rolling, 1);
                        rand.Add(State.Shaking, 4);
                        CurrentState = rand.Get();
                        Timer = 0;
                        Projectile.netUpdate = true;
                    }
                    break;
                }
            case State.Shaking:
                {
                    if (Timer >= 300)
                    {
                        // 固定切换至滚动
                        CurrentState = State.Rolling;
                        Timer = 150;
                        Projectile.netUpdate = true;
                    }

                    break;
                }
        }
    }
    public override void AI()
    {
        if (!CheckActive())                     // 用于判定宠物是否应当继续存在以及timeLeft的锁定等
        {
            currentChatRoom?.CloseChatRoom();   // 如果应当消失就关闭聊天室
            return;
        }
        ControlMovement();                      // 用于更新宠物的移动
        UpdateState();                          // 用于更新宠物自身状态
    }
    #endregion

    #region 对话设置
    public override ChatSettingConfig ChatSettingConfig => base.ChatSettingConfig with
    {
        TextBoardColor = Main.DiscoColor,       // 实际对话颜色是按开始对话时获取的颜色
        TextColor = Color.Black                 // 文本内部全黑
    };
    public override void RegisterChat(ref string name, ref Vector2 indexRange)
    {
        name = nameof(MyPet);
        indexRange = new(0, 3);
    }
    protected override string ChatKeyToRegister(string name, int index)
    {
        var result = this.GetLocalizationKey($"ChatText_{index}");  // 通过弹幕实例获取完整本地化键
        Language.GetOrRegister(result);                             // 自动注册本地化键，不需要可以省略
        return result;
    }
    public override void SetRegularDialog(ref int timePerDialog, ref int chance, ref bool whenShouldStop)
    {
        timePerDialog = 180;            // 每180帧尝试发起对话
        chance = 2;                     // 每次有1/2概率说话
        whenShouldStop = Main.dayTime;  // 白天不说话
    }
    public override WeightedRandom<LocalizedText> RegularDialogText()
    {
        var result = new WeightedRandom<LocalizedText>();
        for (int n = 0; n <= 3; n++)
            result.Add(ChatDictionary[n], n * n + 1);    // 权重写着玩的
        return result;
    }

    public override List<List<ChatRoomInfo>> RegisterChatRoom()
    {
        var myPet = UniqueIDExtended;
        var satori = TouhouPetID.Satori;
        var koishi = ModTouhouPetLoader.UniqueID<Koishi>();
        return [
            [new(myPet,ChatDictionary[0],-1),new(satori, ModUtils.GetChatText("Satori", 4), 0),new(koishi, ModUtils.GetChatText("Koishi", 5), 1)],
            [new(myPet, ChatDictionary[1], -1),new(koishi, ModUtils.GetChatText("Koishi", 5), 0)]
            ];
        // 仅作示例
    }
    #endregion
}
