using LenenPets.Content.Pets;
using LenenPets.Content.PetsAnimations.Core;

namespace LenenPets.Content.PetsAnimations.PetCharacterAnimations;

public class IdleAnimation : CharacterAnimation
{
    protected override void OnActive(BasicLenenPet pet)
    {
        pet.Projectile.frame = FrameIndexMin;
    }
}