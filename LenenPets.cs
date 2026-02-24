global using LenenPets.Tools;
global using Terraria;
global using Terraria.ID;
global using Terraria.ModLoader;
global using static Terraria.ModLoader.ModContent;

namespace LenenPets;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public class LenenPets : Mod
{
    public static LenenPets Instance { get; private set; }

    public override void Load()
    {
        Instance = this;
        base.Load();
    }
}