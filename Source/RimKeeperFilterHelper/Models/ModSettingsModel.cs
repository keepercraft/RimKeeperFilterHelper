using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper.Models
{
    public class RimKeeperFilterHelperModSettings : ModSettings
    {
        public static bool WindowTrigger = false;

        public static bool DebugLog = true;
        public static bool ActiveResize = true;
        public static bool ActiveBookmark = true;
        public static bool ActiveSelection = true;
        public static bool ActivePanelList = false;

        public static int WindowResizeMinX = 200;
        public static int WindowResizeMinY = 200;
        public static int BookmarkWidth = 300;
        public static Vector2 WindowSize = new Vector2(600f, 500f);
        public static Vector2 WindowStorageSize
        {
            get
            {
                if (ActiveBookmark)
                {
                    return new Vector2(
                        WindowSize.x - BookmarkWidth,
                        WindowSize.y);
                }
                return WindowSize;
            }
        }

        public static List<ITab_Storage_Filter_Model> Filters = new List<ITab_Storage_Filter_Model>();

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref DebugLog, nameof(DebugLog), false);
            Scribe_Values.Look(ref ActiveResize, nameof(ActiveResize), true);
            Scribe_Values.Look(ref ActiveBookmark, nameof(ActiveBookmark), true);
            Scribe_Values.Look(ref ActiveSelection, nameof(ActiveSelection), true);
            Scribe_Values.Look(ref ActivePanelList, nameof(ActivePanelList), false);
            Scribe_Collections.Look(ref Filters, nameof(Filters), LookMode.Deep);

            Scribe_Values.Look(ref WindowResizeMinX, nameof(WindowResizeMinX), 200);
            Scribe_Values.Look(ref WindowResizeMinY, nameof(WindowResizeMinY), 200);
            Scribe_Values.Look(ref BookmarkWidth, nameof(BookmarkWidth), 300);
            Scribe_Values.Look(ref WindowSize, nameof(WindowSize), new Vector2(600f, 500f));

            if (Filters.NullOrEmpty())
            {
                Filters = new List<ITab_Storage_Filter_Model>();
            }
        }
    }
}
