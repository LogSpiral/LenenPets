using System.Collections.Generic;
using Terraria.Localization;
using TouhouPets;
using TouhouPets.Content.Projectiles.Pets;

namespace LenenPets.Content.Pets.YabusameHoulen;

public class YabusameChatSystem : ModSystem
{
    public override void PostSetupContent()
    {
        RegisterChat_KoishiToYabusame();
        base.PostSetupContent();
    }

    static void RegisterChat_KoishiToYabusame()
    {
        List<(int, LocalizedText)> list =
        [
            (Koishi.PetID(), YabusameHoulen.GetLocalization("KoishiGreetingsToYabusame")),
            (YabusameHoulen.PetID(), YabusameHoulen.GetLocalization("YabusameReplyToKoishi")),
        ];
        LenenPetsHelper.PetChatRoomFullRegister(list,null,3);
    }
}
