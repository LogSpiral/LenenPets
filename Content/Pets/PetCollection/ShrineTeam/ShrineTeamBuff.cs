using TouhouPets.Content.Buffs;
using TouhouPets.Content.Projectiles.Pets;
using TouhouPets;
using LenenPets.Content.Pets.YabusameHoulen;

namespace LenenPets.Content.Pets.PetCollection.ShrineTeam;

public class ShrineTeamBuff : BasicPetBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.SpawnPetAndSetBuffTime(buffIndex, ProjectileType<YabusameHoulen.YabusameHoulen>());
        player.SpawnPetAndSetBuffTime(buffIndex, ProjectileType<TsubakuraEnraku.TsubakuraEnraku>());
        player.SpawnPetAndSetBuffTime(buffIndex, ProjectileType<Shion.Shion>());
    }
}
