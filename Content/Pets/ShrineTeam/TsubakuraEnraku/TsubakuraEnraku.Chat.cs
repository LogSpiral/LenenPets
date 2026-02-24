using LenenPets.Content.PetsAnimations.PetCharacterAnimations;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.Utilities;
using TouhouPets;

namespace LenenPets.Content.Pets.ShrineTeam.TsubakuraEnraku;

public partial class TsubakuraEnraku
{
    public override ChatSettingConfig ChatSettingConfig =>
    new ChatSettingConfig() with
    {
        TextColor = Color.DarkGray
    };

    public override void RegisterChat(ref string name, ref Vector2 indexRange)
    {
        name = "Ysubakura";
        indexRange = new Vector2(0, 2);
    }

    public override void SetRegularDialog(ref int timePerDialog, ref int chance, ref bool whenShouldStop)
    {
        timePerDialog = 1081;
        chance = 8;
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