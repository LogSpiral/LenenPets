using LenenPets.Content.Pets;
using LenenPets.Content.PetsAnimations.Core;
using Microsoft.Xna.Framework;

namespace LenenPets.Content.PetsAnimations.ExtraAnimations;

public class ShionHitaikakushiAnimation : PetExtraAnimation
{
    public override void Draw(BasicLenenPet pet, Color lightColor)
    {
        var originPos = DrawConfig.PositionOffset;

        base.Draw(pet, (lightColor * .5f) with { A = 192 } * .5f);
        for (int n = 0; n < 4; n++)
        {
            DrawConfig = DrawConfig with { PositionOffset = originPos + (MathHelper.PiOver2 * n + Main.GlobalTimeWrappedHourly).ToRotationVector2() };
            base.Draw(pet, (lightColor * .25f) with { A = 0 });
        }
        DrawConfig = DrawConfig with { PositionOffset = originPos };
    }
}
