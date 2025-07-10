using System;
using System.Collections.Generic;
using Terraria.Localization;
using TouhouPets;
using TouhouPets.Common.ModSupports.ModPetRegisterSystem;

namespace LenenPets.Tools;

public static class LenenPetsHelper
{
    extension<T>(T) where T : BasicTouhouPet
    {
        public static int PetID() => ModTouhouPetLoader.TouhouPetType<T>();

        public static LocalizedText GetLocalization(string suffix) => GetInstance<T>().GetLocalization(suffix);
    }

    public static void PetDialog(int uniqueID, LocalizedText text, Func<bool> condition = null, int weight = 1) 
    {
        TouhouPets.TouhouPets.Instance.Call(nameof(PetDialog),LenenPets.Instance, uniqueID, text, condition, weight);
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
        TouhouPets.TouhouPets.Instance.Call(nameof(PetChatRoom), LenenPets.Instance, realList);
    }

    public static void PetChatRoomFullRegister(List<(int, LocalizedText)> chatRoomInfoList, Func<bool> condition = null, int weight = 1) 
    {
        if (chatRoomInfoList.Count is 0) return;
        var (id, text) = chatRoomInfoList[0];
        PetDialog(id, text, condition, weight);
        PetChatRoom(chatRoomInfoList);
    }
}
