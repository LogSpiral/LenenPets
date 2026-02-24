using TouhouPets.Content.Buffs;

namespace LenenPets.Content.Pets.ShrineTeam.YabusameHoulen;

public class YabusameBuff : BasicPetBuff
{
    public override int PetType => ProjectileType<YabusameHoulen>();
    public override bool LightPet => true;
}