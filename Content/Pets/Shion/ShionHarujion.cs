using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TouhouPets;
namespace LenenPets.Content.Pets.Shion;

public class ShionHarujion : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToVanitypet(ProjectileType<Shion>(), BuffType<ShionBuff>());
        Item.DefaultToVanitypetExtra(26, 34);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (!player.HasBuff(BuffType<ShionBuff>()))
            player.AddBuff(Item.buffType, 2);
        return false;
    }
}
