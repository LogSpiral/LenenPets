using TouhouPets.Content.Buffs;

namespace LenenPets.Content.Pets.Extra.TsubakurowaHaruchou;

public class TsubakurowaBuff : BasicPetBuff
{
    public override int PetType => ProjectileType<TsubakurowaHaruchou>();
    public override bool LightPet => true;
}