using LenenPets.Content.PetsAnimations.PetCharacterAnimations;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.Utilities;
using TouhouPets;

namespace LenenPets.Content.Pets.YabusameHoulen;

public partial class YabusameHoulen
{
    public override ChatSettingConfig ChatSettingConfig =>
        new ChatSettingConfig() with
        {
            TextColor = Color.LightGray
        };

    public override void RegisterChat(ref string name, ref Vector2 indexRange)
    {
        name = "Yabusame";
        indexRange = new Vector2(0, 2);
    }
    public override void SetRegularDialog(ref int timePerDialog, ref int chance, ref bool whenShouldStop)
    {
        timePerDialog = 721;
        chance = 6;
        whenShouldStop = CharacterAnimation is not IdleAnimation _;
    }
    public override WeightedRandom<LocalizedText> RegularDialogText()
    {
        WeightedRandom<LocalizedText> chat = new();
        for (int n = 0; n < 3; n++)
            chat.Add(ChatDictionary[n]);
        return chat;
    }
}
