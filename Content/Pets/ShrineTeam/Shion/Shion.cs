using LenenPets.Content.Pets.PetCollection.ShrineTeam;
using LenenPets.Content.PetsStates.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LenenPets.Content.Pets.ShrineTeam.Shion;

public partial class Shion : BasicLenenPet
{
    public override void PetStaticDefaults()
    {
        Main.projFrames[Type] = 5;
        Main.projPet[Type] = true;
        ProjectileID.Sets.LightPet[Type] = true;

        ProjectileID.Sets.CharacterPreviewAnimations[Type] =
            ProjectileID.Sets.SimpleLoop(0, 1);
    }

    public override void PetDefaults()
    {
        base.PetDefaults();
        CharacterAnimation = IdleAnimation;
        IdleAnimation.SetActive();
        ClothAnimation.SetActive();
        HitaikakushiAnimation.SetActive();
        WingAnimation.SetActive();
    }

    protected override bool CheckActive()
    {
        Projectile.timeLeft = 2;
        var player = Owner;
        bool selfBuff = player.HasBuff<ShionBuff>();
        bool teamBuff = player.HasBuff<ShrineTeamBuff>();
        IdleState.PositionOffset = teamBuff ? new Vector2(0, -64) : new Vector2(56, -34);
        if (!(selfBuff || teamBuff) || player.dead)
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
        CharacterClothAnimation.Frame = Projectile.frame;
    }

    protected override IReadOnlyList<IPetState> PetStates => [IdleState];
}