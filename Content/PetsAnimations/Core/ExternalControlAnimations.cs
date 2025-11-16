using LenenPets.Content.Pets;
using Microsoft.Xna.Framework;
using TouhouPets;

namespace LenenPets.Content.PetsAnimations.Core
{
    internal class ExternalControlAnimations : IPetAnimation
    {
        public required DrawPetConfig DrawConfig { get; set; }
        public int Frame { get; set; }
        public required int Row { get; set; }
        public bool Active { get; set; } = true;
        public void Draw(BasicLenenPet pet, Color lightColor)
        {
            if (Active)
                pet.Projectile.DrawPet(
                    pet.Projectile.frame,
                    lightColor,
                    DrawConfig,
                    Row);
        }

        public void Update(BasicLenenPet pet)
        {
        }
        bool IPetAnimation.ShaderRequired => DrawConfig.ShouldUseEntitySpriteDraw;
    }
}
