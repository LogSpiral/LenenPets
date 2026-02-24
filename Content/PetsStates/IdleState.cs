using LenenPets.Content.Pets;
using LenenPets.Content.PetsStates.Core;
using Microsoft.Xna.Framework;

namespace LenenPets.Content.PetsStates;

public class IdleState : IPetState
{
    void IPetState.Update(BasicLenenPet pet)
    {
        pet.Projectile.tileCollide = false;
        pet.Projectile.rotation = pet.Projectile.velocity.X * RotationFactor;

        pet.ChangeDir();

        Vector2 point = new(PositionOffset.X * pet.Owner.direction, PositionOffset.Y + pet.Owner.gfxOffY);

        if (!pet.Owner.dead)
            pet.MoveToPoint(point, SpeedFactor, IdleCenter ?? Vector2.Zero);
    }

    public Vector2 PositionOffset { get; set; }

    public float RotationFactor { get; set; }

    public float SpeedFactor { get; set; }

    public Vector2? IdleCenter { get; set; } = null;
}