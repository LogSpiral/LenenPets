using LenenPets.Content.PetsAnimations.PetCharacterAnimations;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.Utilities;
using TouhouPets;

namespace LenenPets.Content.Pets.Extra.TsubakurowaHaruchou;

public partial class TsubakurowaHaruchou
{
    public override ChatSettingConfig ChatSettingConfig =>
    new ChatSettingConfig() with
    {
        TextColor = Color.DarkGray
    };

    public override void RegisterChat(ref string name, ref Vector2 indexRange)
    {
        name = "Tsubakurowa";
        indexRange = Vector2.Zero;
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
        return chat;
    }
}