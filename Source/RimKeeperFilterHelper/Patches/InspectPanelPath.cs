using HarmonyLib;
using Keepercraft.RimKeeperFilterHelper.Models;
using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper.Patches
{
    [HarmonyPatch(typeof(InspectPaneFiller), "DoPaneContentsFor")]
    public static class DoPaneContentsFor_InspectPanelPathc
    {
        public static Vector2 scroll = Vector2.zero;
        public static float scrollViewHeight;
        public static float num_h = 22f;
        public static float topgap = 25f;

        static void Postfix(InspectPaneFiller __instance, ISelectable sel, Rect rect)
        {
            if (!RimKeeperFilterHelperModSettings.ActivePanelList) return;
            if (sel is Zone_Stockpile stockpile)
            {
                Rect outRect = new Rect(rect.x, rect.y + topgap, rect.width, rect.height - topgap).ContractedBy(0f);

                //Widgets.DrawMenuSection(outRect);

                Rect scrollRect = new Rect(outRect.x, outRect.y, outRect.width - 16f, scrollViewHeight);
                Widgets.BeginScrollView(outRect, ref scroll, scrollRect, true);
                float num = outRect.y;

                foreach (var item in stockpile.AllContainedThings
                    .Where(c => c.def.EverStorable(false))
                    .GroupBy(c => c.def)
                    .OrderByDescending(c => c.Select(s => s.stackCount).Sum())
                    )
                {
                    Rect itemRect = new Rect(outRect.x + num_h + 5f, num, scrollRect.width - num_h - 5f, num_h);

                    GUI.DrawTexture(
                        new Rect(outRect.x, num, num_h, num_h),
                        item.Key.uiIcon
                        );

                    Widgets.Label(itemRect, string.Format("[{1}] {0}", 
                        item.Key.LabelCap,
                        item.Select(s => s.stackCount).Sum()
                        ));

                    num += num_h;
                }

                if (Event.current.type == EventType.Layout)
                {
                    scrollViewHeight = num;
                }
                Widgets.EndScrollView();
            }
        }
    }
}
