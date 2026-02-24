using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;
using TouhouPets;
using TouhouPets.Common.ModSupports.ModPetRegisterSystem;

namespace LenenPets.Tools;

public static class LenenPetsHelper
{
    extension<T>(T) where T : BasicTouhouPet
    {
        public static int PetID() => ModTouhouPetLoader.UniqueID<T>();

        public static LocalizedText GetLocalization(string suffix) => GetInstance<T>().GetLocalization(suffix);
    }

    public static void PetDialog(int uniqueID, LocalizedText text, Func<bool> condition = null, int weight = 1)
    {
        TouhouPets.TouhouPets.CrossModSupport.PetDialog(LenenPets.Instance, uniqueID, text, condition, weight);
    }

    public static void PetDialog(TouhouPetID uniqueID, LocalizedText text, Func<bool> condition = null, int weight = 1)
    {
        PetDialog((int)uniqueID, text, condition, weight);
    }

    public static void PetChatRoom(List<(int, LocalizedText)> chatRoomInfoList)
    {
        List<(int, LocalizedText, int)> realList = [];
        int turn = -1;
        foreach (var (id, text) in chatRoomInfoList)
            realList.Add((id, text, turn++));

        TouhouPets.TouhouPets.CrossModSupport.PetChatRoom(LenenPets.Instance, realList);
    }

    public static void PetChatRoomFullRegister(List<(int, LocalizedText)> chatRoomInfoList, Func<bool> condition = null, int weight = 1, bool allMemberRequired = true)
    {
        if (chatRoomInfoList.Count is 0) return;
        var (id, text) = chatRoomInfoList[0];
        Func<bool> finalCondition;

        if (!allMemberRequired)
            finalCondition = condition;
        else
        {
            HashSet<int> requiredPetsCache = [.. from pair in chatRoomInfoList select pair.Item1];
            bool MemberCondition()
            {
                HashSet<int> requiredPets = [.. requiredPetsCache];
                foreach (var proj in Main.projectile)
                {
                    if (proj.ModProjectile is BasicTouhouPet pet)
                        requiredPets.Remove(pet.UniqueIDExtended);
                    else continue;
                    if (requiredPets.Count == 0)
                        return true;
                }
                return requiredPets.Count == 0;
            }
            if (condition == null)
                finalCondition = MemberCondition;
            else
                finalCondition = () => condition.Invoke() && MemberCondition();
        }
        PetDialog(id, text, finalCondition, weight);
        PetChatRoom(chatRoomInfoList);
    }
}