using LenenPets.Content.Pets;
using LenenPets.Content.PetsAnimations.Core;
using Microsoft.Xna.Framework;

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
}
