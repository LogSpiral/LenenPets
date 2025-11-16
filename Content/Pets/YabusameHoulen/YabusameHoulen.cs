using LenenPets.Content.Pets.PetCollection.ShrineTeam;
using LenenPets.Content.PetsStates.Core;
using System.Collections.Generic;

namespace LenenPets.Content.Pets.YabusameHoulen;

public partial class YabusameHoulen : BasicLenenPet
{
    private int ActionCD
    {
        get => (int)Projectile.localAI[0];
        set => Projectile.localAI[0] = value;
    }
    public override void PetStaticDefaults()
    {
        Main.projFrames[Type] = 5;
        Main.projPet[Type] = true;
        ProjectileID.Sets.LightPet[Type] = true;

        ProjectileID.Sets.CharacterPreviewAnimations[Type] =
            ProjectileID.Sets.SimpleLoop(0, 1)
            .WithCode(OpenDimensionInMenu);
    }

    public override void PetDefaults()
    {
        base.PetDefaults();
        CharacterAnimation = IdleAnimation;
        IdleAnimation.SetActive();
        ClothAnimation.SetActive();
    }

    private void OpenDimensionInMenu(Projectile proj, bool walking)
    {
        // TODO 实现开启次元的Shader效果，并在主页面循环播放
    }

    protected override bool CheckActive()
    {
        Projectile.timeLeft = 2;
        var player = Owner;

        if (!(player.HasBuff<YabusameBuff>() || player.HasBuff<ShrineTeamBuff>()) || player.dead)
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

    protected override void UpdateStatus()
    {

        if (CharacterAnimation == IdleAnimation)
        {
            ActionCD--;
            if (mainTimer % 270 == 0)
                BlinkAnimation.SetActive();

            if (OwnerIsMyPlayer && mainTimer % 360 == 0 && currentChatRoom == null && ActionCD <= 0 && Main.rand.NextBool(2))
            {
                AnnoyingStart();
                ActionCD = 1800;
                Projectile.netUpdate = true;
            }
        }
        if (CharacterAnimation == AnnoyingAnimation && CharacterAnimation.IsFinished)
        {
            CharacterAnimation = IdleAnimation;
            AnnoyingSoundAnimation.SetDeactive();
            IdleAnimation.SetActive();
        }
        CharacterClothAnimation.Frame = Projectile.frame;
    }
    private void AnnoyingStart()
    {
        AnnoyingAnimation.SetActive();
        BlinkAnimation.SetDeactive();
        AnnoyingSoundAnimation.SetActive();
        CharacterAnimation = AnnoyingAnimation;
    }
    protected override IReadOnlyList<IPetState> PetStates => [IdleState];
}