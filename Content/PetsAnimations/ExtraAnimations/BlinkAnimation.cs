using LenenPets.Content.Pets;
using LenenPets.Content.PetsAnimations.Core;
using Microsoft.Xna.Framework;
using TouhouPets;

namespace LenenPets.Content.PetsAnimations.ExtraAnimations;

public class BlinkAnimation : PetExtraAnimation
{
    protected override void OnActive(BasicLenenPet pet)
    {
        frameCounter = 0;
        frameIndex = FrameIndexMin;
    }

    public override void Update(BasicLenenPet pet)
    {
        DefaultUpdate(pet);
        if (frameCounter == 0 && frameIndex == FrameIndexMin)
            SetDeactive();
    }

    public override void Draw(BasicLenenPet pet, Color lightColor)
    {
        if (IsActive || ForceDraw)
            pet.Projectile.DrawPet(
                frameIndex,
                lightColor,
                DrawConfig,
                ExtraAnimationRow);
    }

    public bool ForceDraw { get; set; }
}