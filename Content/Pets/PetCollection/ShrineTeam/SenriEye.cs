using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using TouhouPets.Content.Buffs.PetBuffs;
using TouhouPets.Content.Items.PetItems;
using TouhouPets.Content.Projectiles.Pets;
using TouhouPets;
using LenenPets.Content.Pets.TsubakuraEnraku;
using LenenPets.Content.Pets.YabusameHoulen;
using LenenPets.Content.Pets.Shion;

namespace LenenPets.Content.Pets.PetCollection.ShrineTeam;

public class SenriEye : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToVanitypet(ProjectileType<YabusameHoulen.YabusameHoulen>(), BuffType<ShrineTeamBuff>());
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
