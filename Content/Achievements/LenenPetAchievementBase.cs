using Terraria.Achievements;

namespace LenenPets.Content.Achievements;

public abstract class LenenPetAchievementBase<T>() : ModAchievement where T : ModItem
{
    public override void SetStaticDefaults()
    {
        AddItemPickupCondition(ItemType<T>());
        Achievement.SetCategory(AchievementCategory.Collector);
    }
}
