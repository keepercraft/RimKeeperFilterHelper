using HarmonyLib;
using Keepercraft.RimKeeperFilterHelper.Extensions;
using Keepercraft.RimKeeperFilterHelper.Models;
using RimWorld;
using UnityEngine;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper.Patches
{
    [HarmonyPatch(typeof(ITab_Storage), "FillTab")]
    public static class FillTab_StorageResizePatch
    {
        public static bool resizeActivator = false;
        public static Vector2 resizeStart = Vector2.zero;

        static bool Prefix(ITab_Storage __instance)
        {
            if (!RimKeeperFilterHelperModSettings.ActiveResize) return true;

            Vector2 size = __instance.GetPrivateField<Vector2>("size"); //new Vector2(600f, 780f);

            Rect rect = new Rect(0f, 0f, size.x, 10f);
            GUI.BeginGroup(rect);
            Widgets.DrawHighlightIfMouseover(rect);
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                resizeActivator = true;
                resizeStart = UnityEngine.Input.mousePosition;
                Event.current.Use();
            }

            if (resizeActivator && Input.GetMouseButtonUp(0))
            {
                float xmin = RimKeeperFilterHelperModSettings.WindowResizeMinX;
                if (RimKeeperFilterHelperModSettings.ActiveBookmark)
                {
                    xmin += RimKeeperFilterHelperModSettings.BookmarkWidth;
                }

                resizeActivator = false;
                var y = size.y - (resizeStart.y - Input.mousePosition.y) / Prefs.UIScale; //(WinSize.normalized.x / WinSize.normalized.y);
                var x = size.x - (resizeStart.x - Input.mousePosition.x) / Prefs.UIScale;
                y = Mathf.Max(RimKeeperFilterHelperModSettings.WindowResizeMinY, y);
                x = Mathf.Max(xmin, x);

                var paneTopY = __instance.GetPrivateProperty<float>("PaneTopY");
                y = Mathf.Min(y, paneTopY - 30f);
                x = Mathf.Min(x, Screen.width / Prefs.UIScale);

                RimKeeperFilterHelperModSettings.WindowSize = new Vector2(x, y);
                __instance.SetPrivateField("size", size);
                __instance.SetPrivateStaticField("WinSize", RimKeeperFilterHelperModSettings.WindowStorageSize);
            }
            GUI.EndGroup();
            return true;
        }
    }

    [HarmonyPatch(typeof(InspectTabBase), "TabUpdate")]
    public static class UpdateSize_StorageResizePatch
    {
        static bool trigger = false;
        static bool Prefix(InspectTabBase __instance)
        {
            if (!(__instance is ITab_Storage)) return true;

            var size = __instance.GetPrivateField<Vector2>("size");
            var value = RimKeeperFilterHelperModSettings.WindowSize;
            if (value != Vector2.zero && value != size)
            {
                __instance.SetPrivateField("size", value);
                __instance.SetPrivateStaticField("WinSize", RimKeeperFilterHelperModSettings.WindowStorageSize);
                return true;
            }

            var sizeW = __instance.GetPrivateStaticField<Vector2>("WinSize");
            var ba = RimKeeperFilterHelperModSettings.ActiveBookmark;
            if ((ba && size == sizeW) || (!ba && size != sizeW))
            {
                var b = RimKeeperFilterHelperModSettings.BookmarkWidth;
                RimKeeperFilterHelperModSettings.WindowSize = new Vector2(
                    size.x + (ba ? b : -b),
                    size.y);
                __instance.SetPrivateField("size", RimKeeperFilterHelperModSettings.WindowSize);
                __instance.SetPrivateStaticField("WinSize", RimKeeperFilterHelperModSettings.WindowStorageSize);
                return true;
            }

            return true;
        }
    }
}
