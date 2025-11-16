using System.Collections.Generic;
using Terraria.Localization;
using TouhouPets.Content.Projectiles.Pets;
using Tsubakura = LenenPets.Content.Pets.TsubakuraEnraku.TsubakuraEnraku;

namespace LenenPets.Content.Pets.YabusameHoulen;

public class YabusameChatSystem : ModSystem
{
    public override void PostSetupContent()
    {
        RegisterChat_KoishiToYabusame();
        RegisterChat_TsubakuraToYabusame();
        base.PostSetupContent();
    }

    static void RegisterChat_KoishiToYabusame()
    {
        List<(int, LocalizedText)> list =
        [
            (Koishi.PetID(), YabusameHoulen.GetLocalization("KoishiGreetingsToYabusame")),
            (YabusameHoulen.PetID(), YabusameHoulen.GetLocalization("YabusameReplyToKoishi")),
        ];
        LenenPetsHelper.PetChatRoomFullRegister(list, null, 5);
    }

    static void RegisterChat_TsubakuraToYabusame()
    {
        List<(int, LocalizedText)> list =
        [
            (Tsubakura.PetID(), Tsubakura.GetLocalization("FindYabusame")),
            (YabusameHoulen.PetID(), Tsubakura.GetLocalization("YabusameReply")),
            (Tsubakura.PetID(), Tsubakura.GetLocalization("Reminder")),
            (YabusameHoulen.PetID(), Tsubakura.GetLocalization("YabusameReply2")),
            (Tsubakura.PetID(), Tsubakura.GetLocalization("WishToExit")),
        ];
        LenenPetsHelper.PetChatRoomFullRegister(list, null, 1);
    }
}
