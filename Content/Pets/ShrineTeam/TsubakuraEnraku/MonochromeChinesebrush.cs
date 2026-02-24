using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TouhouPets;

namespace LenenPets.Content.Pets.ShrineTeam.TsubakuraEnraku;

public class MonochromeChinesebrush : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ShimmerTransformToItem[ItemID.DarkShard] = Type;
    }

    public override void SetDefaults()
    {
        Item.DefaultToVanitypet(ProjectileType<TsubakuraEnraku>(), BuffType<TsubakuraBuff>());
        Item.DefaultToVanitypetExtra(22, 42);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (!player.HasBuff(BuffType<TsubakuraBuff>()))
            player.AddBuff(Item.buffType, 2);
        return false;
    }
}