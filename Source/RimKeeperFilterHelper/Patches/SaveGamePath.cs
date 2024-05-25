using HarmonyLib;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper.Patches
{
    [HarmonyPatch(typeof(GameDataSaveLoader), "SaveGame")]
    public static class GameDataSaveLoader_SaveGame
    {
        static void Postfix()
        {
            LoadedModManager.GetMod<RimKeeperFilterHelperMod>().WriteSettings();
        }
    }
}