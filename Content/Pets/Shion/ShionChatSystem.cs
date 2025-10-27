using System.Collections.Generic;
using Terraria.Localization;
using Tsubakura = LenenPets.Content.Pets.TsubakuraEnraku.TsubakuraEnraku;
using Yabusame = LenenPets.Content.Pets.YabusameHoulen.YabusameHoulen;
namespace LenenPets.Content.Pets.Shion;

public class ShionChatSystem : ModSystem
{
    public override void PostSetupContent()
    {
        RegisterChat_Tsubakura_1();
        RegisterChat_Tsubakura_2();
        RegisterChat_ShrineTeam();
        base.PostSetupContent();
    }

    static void RegisterChat_Tsubakura_1()
    {
        List<(int, LocalizedText)> list =
        [
            (Tsubakura.PetID(), Shion.GetLocalization("Dialogue_Tsubakura_1_1")),
            (Shion.PetID(), Shion.GetLocalization("Dialogue_Tsubakura_1_2")),
            (Tsubakura.PetID(), Shion.GetLocalization("Dialogue_Tsubakura_1_3"))
        ];
        LenenPetsHelper.PetChatRoomFullRegister(list, null, 2);
    }
    static void RegisterChat_Tsubakura_2()
    {
        List<(int, LocalizedText)> list =
        [
            (Shion.PetID(), Shion.GetLocalization("Dialogue_Tsubakura_2_1")),
            (Tsubakura.PetID(), Shion.GetLocalization("Dialogue_Tsubakura_2_2")),
            (Shion.PetID(), Shion.GetLocalization("Dialogue_Tsubakura_2_3")),
            (Tsubakura.PetID(), Shion.GetLocalization("Dialogue_Tsubakura_2_4"))
        ];
        LenenPetsHelper.PetChatRoomFullRegister(list, null, 2);
    }

    static void RegisterChat_ShrineTeam()
    {
        List<(int, LocalizedText)> list =
        [
            (Shion.PetID(), Shion.GetLocalization("Dialogue_ShrineTeam_1")),
            (Yabusame.PetID(), Shion.GetLocalization("Dialogue_ShrineTeam_2")),
            (Tsubakura.PetID(), Shion.GetLocalization("Dialogue_ShrineTeam_3")),
            (Shion.PetID(), Shion.GetLocalization("Dialogue_ShrineTeam_4")),
            (Tsubakura.PetID(), Shion.GetLocalization("Dialogue_ShrineTeam_5")),
            (Yabusame.PetID(), Shion.GetLocalization("Dialogue_ShrineTeam_6")),
            (Shion.PetID(), Shion.GetLocalization("Dialogue_ShrineTeam_7")),
            (Tsubakura.PetID(), Shion.GetLocalization("Dialogue_ShrineTeam_8")),
            (Yabusame.PetID(), Shion.GetLocalization("Dialogue_ShrineTeam_9")),
            (Shion.PetID(), Shion.GetLocalization("Dialogue_ShrineTeam_10"))
        ];
        LenenPetsHelper.PetChatRoomFullRegister(list, null, 1);
    }
}
