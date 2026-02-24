using LenenPets.Content.Pets.ShrineTeam.Shion;
using LenenPets.Content.Pets.ShrineTeam.TsubakuraEnraku;
using LenenPets.Content.Pets.ShrineTeam.YabusameHoulen;
using TouhouPets;
using TouhouPets.Content.Buffs;

namespace LenenPets.Content.Pets.PetCollection.ShrineTeam;

public class ShrineTeamBuff : BasicPetBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.SpawnPetAndSetBuffTime(buffIndex, ProjectileType<YabusameHoulen>());
        player.SpawnPetAndSetBuffTime(buffIndex, ProjectileType<TsubakuraEnraku>());
        player.SpawnPetAndSetBuffTime(buffIndex, ProjectileType<Shion>());
    }
}