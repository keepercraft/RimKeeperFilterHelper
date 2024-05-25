using HarmonyLib;
using Keepercraft.RimKeeperFilterHelper.Extensions;
using Keepercraft.RimKeeperFilterHelper.Models;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper.Patches
{
    [HarmonyPatch(typeof(Listing_TreeThingFilter), "DoSpecialFilter")]
    public static class DoSpecialFilter_FilterSelectionPatch
    {
        static bool Prefix(Listing_TreeThingFilter __instance, SpecialThingFilterDef sfDef, int nestLevel)
        {
            if (RimKeeperFilterHelperModSettings.ActiveSelection) return true;
            __instance.FlipAllow(sfDef.saveKey, nestLevel, c => c.FlipAllow(sfDef));
            return true;
        }
    }

    [HarmonyPatch(typeof(Listing_TreeThingFilter), "DoThingDef")]
    public static class DoThingDef_FilterSelectionPatch
    {
        static bool Prefix(Listing_TreeThingFilter __instance, ThingDef tDef, int nestLevel)
        {
            if (!RimKeeperFilterHelperModSettings.ActiveSelection) return true;
            __instance.FlipAllow(tDef.defName, nestLevel, c => c.FlipAllow(tDef));
            return true;
        }
    }

    [HarmonyPatch(typeof(Listing_TreeThingFilter), "DoCategory")]
    public static class DoCategory_FilterSelectionPatch
    {
        static bool Prefix(Listing_TreeThingFilter __instance, TreeNode_ThingCategory node, int indentLevel)
        {
            if (!RimKeeperFilterHelperModSettings.ActiveSelection) return true;
            __instance.FlipAllow(node.Label, indentLevel, c => c.FlipAllow(node));
            return true;
        }
    }
}