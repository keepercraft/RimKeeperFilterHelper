using HarmonyLib;
using Keepercraft.RimKeeperFilterHelper.Extensions;
using Keepercraft.RimKeeperFilterHelper.Helpers;
using Keepercraft.RimKeeperFilterHelper.Models;
using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper.Patches
{
    [HarmonyPatch(typeof(ITab_Storage), "FillTab")]
    public static class FillTab_StorageBookmarkPatch
    {
        public static string bookmarkNewText = "";
        public static Vector2 bookmarkScroll = Vector2.zero;
        public static float bookmarkScrollViewHeight;

        static void Postfix(ITab_Storage __instance)
        {
            if (!RimKeeperFilterHelperModSettings.ActiveBookmark) return;

            var bwidth = RimKeeperFilterHelperModSettings.BookmarkWidth + 18f;
            Vector2 size = __instance.GetPrivateField<Vector2>("size");
            float topAreaHeight = __instance.GetPrivateProperty<float>("TopAreaHeight");
            Rect rect2 = new Rect(size.x - bwidth, topAreaHeight, bwidth, size.y - topAreaHeight).ContractedBy(10f);
            GUI.BeginGroup(rect2);
            Widgets.DrawMenuSection(new Rect(0f, 0f, rect2.width, rect2.height));

            var boxUp = new Rect(0f, 0f, rect2.width, 30f).ContractedBy(3f);

            bookmarkNewText = Widgets.TextField(
                new Rect(boxUp.x + boxUp.height, boxUp.y, rect2.width - boxUp.height - boxUp.height, boxUp.height), 
                bookmarkNewText);
            //if (Widgets.ButtonText(new Rect(rect2.width - boxUp.x - boxUp.height, boxUp.y, boxUp.height, boxUp.height), ">"))
            if (Widgets.ButtonImageWithBG(
                new Rect(rect2.width - boxUp.x - boxUp.height, boxUp.y, boxUp.height, boxUp.height), 
                ContentFinder<Texture2D>.Get("ui/buttons/paste")))
            {
                if (!string.IsNullOrEmpty(bookmarkNewText))
                {
                    var model = __instance
                        .GetPrivateProperty<IStoreSettingsParent>("SelStoreSettingsParent")
                        .GetStoreSettings()
                        .StoreFilter(bookmarkNewText);
                    RimKeeperFilterHelperModSettings.Filters.Add(model);
                    bookmarkNewText = "";
                }
            }

            GUI.DrawTexture(
                new Rect(boxUp.x - 2.5f, boxUp.y - 2.5f, boxUp.height + 5, boxUp.height + 5),
                 ContentFinder<Texture2D>.Get("things/item/book/textbook/textbook")
                );

            // Widgets.ButtonText(new Rect(2f, 2f, 100f, 20f), "UPDATE" + ITab_Storage_Resize_Model.defNames.Count());

            Rect outRect = new Rect(0f, 40f, rect2.width, rect2.height - 25f).ContractedBy(3f);
            Rect scrollRect = new Rect(outRect.x, outRect.y, outRect.width - 16f, bookmarkScrollViewHeight);
            Widgets.BeginScrollView(outRect, ref bookmarkScroll, scrollRect, true);
            float num = outRect.y;

            for (int i = 0; i < RimKeeperFilterHelperModSettings.Filters.Count; i++)
            //foreach (var item in ITab_Storage_Resize_Model.thingFilters)
            {
                var item = RimKeeperFilterHelperModSettings.Filters[i];
                Rect itemRect = new Rect(outRect.x, num, scrollRect.width - 44f, 22f);

                if (Widgets.ButtonText(itemRect, item.Name))
                {
                    DebugHelper.Message("Filter LOAD:" + item.Allowed.Count());
                    __instance
                        .GetPrivateProperty<IStoreSettingsParent>("SelStoreSettingsParent")
                        .GetStoreSettings()
                        .SetFilter(item, true, true);
                }

                //if (Widgets.ButtonText(new Rect(scrollRect.width - 44f, num, 22f, 22f), "+"))
                if (Widgets.ButtonImageWithBG(
                    new Rect(scrollRect.width - 44f, num, 22f, 22f),
                    ContentFinder<Texture2D>.Get("ui/buttons/plus")))

                {
                    DebugHelper.Message("Filter LOAD:" + item.Allowed.Count());
                    __instance
                        .GetPrivateProperty<IStoreSettingsParent>("SelStoreSettingsParent")
                        .GetStoreSettings()
                        .SetFilter(item, true);
                }

                //if (Widgets.ButtonText(new Rect(scrollRect.width - 22f, num, 22f, 22f), "-"))
                if (Widgets.ButtonImageWithBG(
                    new Rect(scrollRect.width - 22f, num, 22f, 22f), 
                    ContentFinder<Texture2D>.Get("ui/buttons/minus")))
                {
                    DebugHelper.Message("Filter LOAD:" + item.Allowed.Count());
                    __instance
                        .GetPrivateProperty<IStoreSettingsParent>("SelStoreSettingsParent")
                        .GetStoreSettings()
                        .SetFilter(item, false);
                }

                //if (Widgets.ButtonText(new Rect(scrollRect.width - 0f, num, 22f, 22f), "X"))
                if (Widgets.ButtonImageWithBG(
                    new Rect(scrollRect.width - 0f, num, 22f, 22f), 
                    ContentFinder<Texture2D>.Get("ui/buttons/dismiss")))
                {
                    RimKeeperFilterHelperModSettings.Filters.RemoveAt(i);
                }
                num += 22f;
            }
            if (Event.current.type == EventType.Layout)
            {
                bookmarkScrollViewHeight = num;
            }
            Widgets.EndScrollView();
            GUI.EndGroup();
        }
    }
}
