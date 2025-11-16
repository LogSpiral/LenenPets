using LenenPets.Content.Pets;
using Microsoft.Xna.Framework;

namespace LenenPets.Content.PetsAnimations.Core;

public interface IPetAnimation
{
    void Draw(BasicLenenPet pet, Color lightColor);

    void Update(BasicLenenPet pet);

    bool ShaderRequired { get; }
}
