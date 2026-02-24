using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent.Achievements;

namespace LenenPets.Content.Achievements;

public class WelcomeToLenenPets : ModAchievement
{
    public static CustomFlagCondition UnlockCondition { get; private set; }
    public override void SetStaticDefaults()
    {
        UnlockCondition = AddCondition("LenenPets: Welcome");
        Achievement.SetCategory(Terraria.Achievements.AchievementCategory.Explorer);
    }
}

public class WelcomeToLenenPetsPlayer : ModPlayer
{
    public override void OnEnterWorld()
    {
        WelcomeToLenenPets.UnlockCondition.Complete();
    }
}