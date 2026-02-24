using TouhouPets.Content.Buffs;

namespace LenenPets.Content.Pets.ShrineTeam.Shion;

public class ShionBuff : BasicPetBuff
{
    public override int PetType => ProjectileType<Shion>();
    public override bool LightPet => true;
}