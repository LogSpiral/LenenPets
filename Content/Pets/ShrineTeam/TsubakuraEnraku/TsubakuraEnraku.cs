using LenenPets.Content.Pets.PetCollection.ShrineTeam;
using LenenPets.Content.PetsAnimations.ExtraAnimations;
using LenenPets.Content.PetsAnimations.PetCharacterAnimations;
using LenenPets.Content.PetsStates.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LenenPets.Content.Pets.ShrineTeam.TsubakuraEnraku;

public partial class TsubakuraEnraku : BasicLenenPet
{
    public override void PetStaticDefaults()
    {
        Main.projFrames[Type] = 5;
        Main.projPet[Type] = true;

        ProjectileID.Sets.CharacterPreviewAnimations[Type] =
            ProjectileID.Sets.SimpleLoop(0, 1);
    }

    public override void PetDefaults()
    {
        base.PetDefaults();
        IdleState.PositionOffset = new Vector2(-56, -34);
        CharacterAnimation = IdleAnimation;
        IdleAnimation.SetActive();
        ClothAnimation.SetActive();
        HatAnimation.SetActive();
    }

    protected override bool CheckActive()
    {
        Projectile.timeLeft = 2;
        var player = Owner;

        if (!(player.HasBuff<TsubakuraBuff>() || player.HasBuff<ShrineTeamBuff>()) || player.dead)
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
        if (mainTimer % 270 == 0)
            BlinkAnimation.SetActive();
        if (mainTimer % 600 == 0)
            SenriBlinkAnimation.SetActive();
        CharacterClothAnimation.Frame = Projectile.frame;
    }

    protected override IReadOnlyList<IPetState> PetStates => [IdleState];
}