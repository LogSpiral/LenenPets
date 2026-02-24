using LenenPets.Content.Pets.ShrineTeam.Shion;
using LenenPets.Content.Pets.ShrineTeam.TsubakuraEnraku;
using LenenPets.Content.Pets.ShrineTeam.YabusameHoulen;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TouhouPets;

namespace LenenPets.Content.Pets.PetCollection.ShrineTeam;

public class SenriEye : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToVanitypet(ProjectileType<YabusameHoulen>(), BuffType<ShrineTeamBuff>());
        Item.DefaultToVanitypetExtra(24, 24);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        player.AddBuff(Item.buffType, 2);
        return false;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
        .AddIngredient<MonochromeChinesebrush>()
        .AddIngredient<YabusameDimensionPiece>()
        .AddIngredient<ShionHarujion>()
        .Register();
    }
}