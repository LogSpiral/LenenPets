using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.Utilities;
using TouhouPets;

namespace LenenPets.Content.Pets.YabusameHoulen;
public class YabusameHoulen : BasicTouhouPet
{
    private enum States
    {
        Idle,
        Blink,
        Calling,
        CallingFadeIn,
        CallingFadeOut,
        Annoying,
        AfterAnnoying,
        Fading,
    }
    private States CurrentState
    {
        get => (States)PetState;
        set => PetState = (int)value;
    }
    private int ActionCD
    {
        get => (int)Projectile.localAI[0];
        set => Projectile.localAI[0] = value;
    }
    private int Timer
    {
        get => (int)Projectile.localAI[1];
        set => Projectile.localAI[1] = value;
    }
    private int RandomCount
    {
        get => (int)Projectile.localAI[2];
        set => Projectile.localAI[2] = value;
    }
    private bool IsIdleState => CurrentState <= States.Blink;

    private int blinkFrame, blinkFrameCounter;
    private int clothFrame, clothFrameCounter;
    private int annoyingFrame, annoyingFrameCounter;
    private Vector2 eyePosition, eyePositionOffset;
    private bool whiteDye;
    private bool yellowBlackDye;

    private DrawPetConfig drawConfig = new(2);
    private readonly Asset<Texture2D> clothTex = ModAsset.YabusameHoulen_Cloth;
    public override void PetStaticDefaults()
    {
        Main.projFrames[Type] = 18;
        Main.projPet[Type] = true;

        ProjectileID.Sets.CharacterPreviewAnimations[Type] =
            ProjectileID.Sets.SimpleLoop(0, 1)
            .WithCode(DisappearOnSelect);
    }
    public override bool OnMouseHover(ref bool dontInvis)
    {
        return false;
    }
    private void DisappearOnSelect(Projectile proj, bool walking)
    {
        if (walking)
        {
            proj.Opacity = MathHelper.Clamp(proj.Opacity += 0.05f, 0, 1);
        }
        else
        {
            proj.Opacity = MathHelper.Clamp(proj.Opacity -= 0.05f, 0, 1);
        }
    }

    public override bool DrawPetSelf(ref Color lightColor)
    {
        bool hasDye = whiteDye || yellowBlackDye;

        Texture2D tex = null;
        Texture2D cloth = clothTex.Value;

        DrawPetConfig config = drawConfig with
        {
            AltTexture = tex,
        };
        DrawPetConfig config2 = config with
        {
            ShouldUseEntitySpriteDraw = !hasDye,
        };

        if (eyePositionOffset.Y <= 0)
            DrawEye(tex, eyePosition - Main.screenPosition, lightColor);

        if (yellowBlackDye)
            Projectile.DrawPet(10, lightColor,
                config with
                {
                    PositionOffset = new Vector2(-2 * Projectile.spriteDirection, 3f * Main.essScale)
                }, 1);

        Projectile.DrawPet(Projectile.frame, lightColor, config);

        if (CurrentState == States.Blink)
            Projectile.DrawPet(blinkFrame, lightColor, config, 1);

        if (!hasDye)
        {
            Projectile.DrawPet(Projectile.frame, lightColor,
            config2 with
            {
                AltTexture = cloth,
            });
        }
        Projectile.DrawPet(clothFrame, lightColor, config2, 1);
        Projectile.ResetDrawStateForPet();

        Projectile.DrawPet(annoyingFrame, lightColor, config, 1);

        if (eyePositionOffset.Y > 0)
            DrawEye(tex, eyePosition - Main.screenPosition, lightColor);
        return false;
    }
    private void DrawEye(Texture2D tex, Vector2 eyePos, Color lightColor)
    {
        Texture2D t = tex ?? TextureAssets.Projectile[Type].Value;
        int height = t.Height / Main.projFrames[Type];
        Rectangle rect = new Rectangle(t.Width / 2, 7 * height, t.Width / 2, height);
        Vector2 orig = rect.Size() / 2;
        Main.spriteBatch.Draw(t, eyePos, rect, Projectile.GetAlpha(lightColor) * mouseOpacity
            , Projectile.rotation, orig, Projectile.scale, SpriteEffects.None, 0f);
    }
    public override ChatSettingConfig ChatSettingConfig => new ChatSettingConfig() with
    {
        TextColor = Color.LightGray//new Color(145, 255, 183),
    };
    protected override string ChatKeyToRegister(string name, int index) => this.GetLocalizationKey($"Regular_{index}");
    public override void RegisterChat(ref string name, ref Vector2 indexRange)
    {
        name = "Yabusame";
        indexRange = new Vector2(0, 2);
    }
    public override void SetRegularDialog(ref int timePerDialog, ref int chance, ref bool whenShouldStop)
    {
        timePerDialog = 721;//721;//721
        chance = 6;//6
        whenShouldStop = !IsIdleState;
    }
    public override WeightedRandom<LocalizedText> RegularDialogText()
    {
        WeightedRandom<LocalizedText> chat = new();
        for (int n = 0; n < 3; n++)
            chat.Add(ChatDictionary[n]);
        return chat;
    }
    public override void VisualEffectForPreview()
    {
        UpdateClothFrame();
        UpdateAnnoyingFrame();
        UpdateEyePosition();
    }
    public override void AI()
    {
        if (!SetYabusameActive(Owner))
        {
            if (currentChatRoom != null)
            {
                currentChatRoom.CloseChatRoom();
            }
            return;
        }

        ControlMovement();

        switch (CurrentState)
        {
            case States.Blink:
                Blink();
                break;

            case States.Calling:
                shouldNotTalking = true;
                Calling();
                break;

            case States.CallingFadeIn:
                shouldNotTalking = true;
                CallingFadeIn();
                break;

            case States.CallingFadeOut:
                shouldNotTalking = true;
                CallingFadeOut();
                break;

            case States.Fading:
                shouldNotTalking = true;
                Fading();
                break;

            case States.Annoying:
                shouldNotTalking = true;
                Annoying();
                break;

            case States.AfterAnnoying:
                shouldNotTalking = true;
                AfterAnnoying();
                break;

            default:
                Idle();
                break;
        }

        if (IsIdleState && ActionCD > 0)
        {
            ActionCD--;
        }

        whiteDye =
            Owner.miscDyes[0].type == ItemID.SilverDye
            || Owner.miscDyes[0].type == ItemID.BrightSilverDye;

        yellowBlackDye = Owner.miscDyes[0].type == ItemID.YellowandBlackDye;
    }
    private void UpdateEyePosition()
    {
        float time = Main.GlobalTimeWrappedHourly * 4f;
        eyePositionOffset = new Vector2(1.2f * (float)Math.Cos(time), 0.35f * (float)Math.Sin(time)) * 26f;
        eyePosition = Projectile.Center + eyePositionOffset + new Vector2(0, 8);
    }
    private bool SetYabusameActive(Player player)
    {
        Projectile.timeLeft = 2;

        bool noActiveBuff = !player.HasBuff(BuffType<YabusameBuff>()) && !player.HasBuff(BuffType<YabusameBuff>());
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
        }
        else
        {
            if (CurrentState != States.Fading)
            {
                if (Projectile.Opacity < 1)
                    Projectile.Opacity += 0.009f;
            }
            return true;
        }
        return false;
    }
    private void ControlMovement()
    {
        Projectile.tileCollide = false;
        Projectile.rotation = Projectile.velocity.X * 0.01f;

        ChangeDir();

        Vector2 point = new(54 * Owner.direction, -34 + Owner.gfxOffY);

        if (!Owner.dead)
            MoveToPoint(point, 13f);
    }
    private void Idle()
    {
        Projectile.frame = 0;
        if (OwnerIsMyPlayer)
        {
            if (mainTimer % 270 == 0)
            {
                CurrentState = States.Blink;
            }
            if (mainTimer > 0 && mainTimer % 360 == 0 && currentChatRoom == null && ActionCD <= 0)
            {
                if (Main.rand.NextBool(4))
                {
                    if (Projectile.CurrentlyNoDialog())
                    {
                        RandomCount = Main.rand.Next(5, 14);
                        CurrentState = States.Annoying;
                    }
                }
            }
            //if (mainTimer > 0 && mainTimer % 180 == 0 && currentChatRoom == null && ActionCD <= 0)
            //{
            //    Projectile.SetChatForChatRoom(this.GetLocalization("KoishiGreetingsToYabusame"));
            //}

        }
    }
    private void Blink()
    {
        Projectile.frame = 0;
        if (blinkFrame < 4)
        {
            blinkFrame = 4;
        }
        if (++blinkFrameCounter > 3)
        {
            blinkFrameCounter = 0;
            blinkFrame++;
        }
        if (blinkFrame > 6)
        {
            blinkFrame = 4;
            PetState = 0;
        }
    }
    private void Fading()
    {
        Projectile.frame = 0;
        Timer++;
        if (Timer > RandomCount - 255 / 2)
        {
            if (Projectile.Opacity < 1)
                Projectile.Opacity += 0.01f;
        }
        else
        {
            if (Projectile.Opacity > 0)
                Projectile.Opacity -= 0.005f;
        }
        if (OwnerIsMyPlayer && Timer > RandomCount)
        {
            Timer = 0;
            ActionCD = 3600;
            CurrentState = States.Idle;
        }
    }
    private void Annoying()
    {
        if (++Projectile.frameCounter > 5)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;
        }
        if (Projectile.frame < 14)
        {
            Projectile.frame = 14;
        }
        if (Projectile.frame > 16)
        {
            Projectile.frame = 15;
            Timer++;
        }
        if (OwnerIsMyPlayer && Timer > RandomCount)
        {
            Timer = 0;
            CurrentState = States.AfterAnnoying;
        }
    }
    private void AfterAnnoying()
    {
        if (++Projectile.frameCounter > 5)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;
        }
        if (Projectile.frame > 17)
        {
            Projectile.frame = 0;
            if (OwnerIsMyPlayer)
            {
                ActionCD = 1800;
                CurrentState = States.Idle;
            }
        }
    }
    private void UpdateClothFrame()
    {
        int count = 4;
        if (++clothFrameCounter > count)
        {
            clothFrameCounter = 0;
            clothFrame++;
        }
        if (clothFrame > 3)
        {
            clothFrame = 0;
        }
    }
    private void UpdateAnnoyingFrame()
    {
        if (Projectile.frame >= 14 && Projectile.frame <= 16)
        {
            if (++annoyingFrameCounter > 4)
            {
                annoyingFrameCounter = 0;
                annoyingFrame++;
            }
            if (annoyingFrame < 8)
            {
                annoyingFrame = 8;
            }
            if (annoyingFrame > 9)
            {
                annoyingFrame = 8;
            }
        }
        else
        {
            annoyingFrame = 11;
        }
    }
    private void Calling()
    {
        if (++Projectile.frameCounter > 12)
        {
            Projectile.frameCounter = 0;
            Projectile.frame++;
        }
        if (Projectile.frame > 6)
        {
            Projectile.frame = 5;
        }

        if (Projectile.frame >= 5)
        {
            if (OwnerIsMyPlayer)
            {
                ChatSettingConfig chatConfig = ChatSettingConfig with
                {
                    TimeLeftPerWord = 60,
                };
                //if (Timer == 0)
                //{
                //    Projectile.SetChat(chatConfig, ChatDictionary[6], 60);
                //}
                //if (Timer == 360)
                //{
                //    Projectile.SetChat(chatConfig, ChatDictionary[7], 60);
                //}
                if (Timer >= 360 + 540)
                {
                    Timer = 0;
                    CurrentState = States.CallingFadeIn;
                    Projectile.CloseCurrentDialog();
                    return;
                }
            }
            Timer++;
        }
    }
    private void CallingFadeIn()
    {
        Projectile.frame = 6;
        Projectile.Opacity -= 0.01f;
        if (Projectile.Opacity <= 0f)
        {
            Projectile.Opacity = 0;
            if (OwnerIsMyPlayer)
            {
                CurrentState = States.CallingFadeOut;
            }
        }
    }
    private void CallingFadeOut()
    {
        if (!Owner.dead)
        {
            Projectile.Center = Owner.Center + new Vector2(-30 * Owner.direction, Owner.gfxOffY);
        }
        Projectile.frame = 7;
        Projectile.Opacity += 0.01f;
        if (Projectile.Opacity >= 1f)
        {
            Projectile.Opacity = 1;
        }
    }
}
