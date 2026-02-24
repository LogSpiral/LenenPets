using LenenPets.Content.Pets.ShrineTeam.TsubakuraEnraku;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TouhouPets;

namespace LenenPets.Content.Pets.Extra.TsubakurowaHaruchou;

public class MonochromeFeather : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ShimmerTransformToItem[ItemType<MonochromeChinesebrush>()] = Type;
    }

    public override void SetDefaults()
    {
        Item.DefaultToVanitypet(ProjectileType<TsubakurowaHaruchou>(), BuffType<TsubakurowaBuff>());
        Item.DefaultToVanitypetExtra(22, 42);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (!player.HasBuff(BuffType<TsubakurowaBuff>()))
            player.AddBuff(Item.buffType, 2);
        return false;
    }
}