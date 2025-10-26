using TouhouPets.Content.Buffs;
namespace LenenPets.Content.Pets.Shion;
public class ShionBuff : BasicPetBuff
{
    public override int PetType => ProjectileType<Shion>();
    public override bool LightPet => true;
}
