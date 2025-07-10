using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TouhouPets;
namespace LenenPets.Content.Pets.YabusameHoulen;

public class YabusameDimensionPiece:ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ShimmerTransformToItem[528] = Type;
    }
    public override void SetDefaults()
    {
        Item.DefaultToVanitypet(ProjectileType<YabusameHoulen>(), BuffType<YabusameBuff>());
        Item.DefaultToVanitypetExtra(26, 34);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (!player.HasBuff(BuffType<YabusameBuff>()))
            player.AddBuff(Item.buffType, 2);
        return false;
    }
}
