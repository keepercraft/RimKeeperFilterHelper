using Keepercraft.RimKeeperFilterHelper.Models;
using UnityEngine;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper
{
    public class RimKeeperFilterHelperMod : Mod
    {
        public string BookmarkWidthText = "0";
        public string WindowResizeMinYText = "0";
        public string WindowResizeMinXText = "0";

        public RimKeeperFilterHelperMod(ModContentPack content) : base(content)
        {
            GetSettings<RimKeeperFilterHelperModSettings>();
            BookmarkWidthText = RimKeeperFilterHelperModSettings.BookmarkWidth.ToString();
            WindowResizeMinYText = RimKeeperFilterHelperModSettings.WindowResizeMinY.ToString();
            WindowResizeMinXText = RimKeeperFilterHelperModSettings.WindowResizeMinX.ToString();
        }

        public override string SettingsCategory() => "RK Storage and Filters";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            Rect newRect = new Rect(inRect.x, inRect.y, inRect.width / 2, inRect.height);
            listingStandard.Begin(newRect);

            listingStandard.CheckboxLabeled("Debug Log", ref RimKeeperFilterHelperModSettings.DebugLog, "Log Messages");
            listingStandard.Gap();

            listingStandard.CheckboxLabeled("Active window resize", ref RimKeeperFilterHelperModSettings.ActiveResize);
            listingStandard.Gap();
            listingStandard.CheckboxLabeled("Active window bookmark", ref RimKeeperFilterHelperModSettings.ActiveBookmark);
            listingStandard.Gap();
            listingStandard.CheckboxLabeled("Active dynamic selection", ref RimKeeperFilterHelperModSettings.ActiveSelection);
            listingStandard.Gap();
            //listingStandard.CheckboxLabeled("Active panel list", ref RimKeeperFilterHelperModSettings.ActivePanelList);
            //listingStandard.Gap();

            listingStandard.Label("Bookmark Width:");
            listingStandard.IntEntry(ref RimKeeperFilterHelperModSettings.BookmarkWidth, ref BookmarkWidthText, 1);
            listingStandard.Gap();

            listingStandard.Label("WindowResize MinY:");
            listingStandard.IntEntry(ref RimKeeperFilterHelperModSettings.WindowResizeMinY, ref WindowResizeMinYText, 1);
            listingStandard.Gap();

            listingStandard.Label("WindowResize MinX:");
            listingStandard.IntEntry(ref RimKeeperFilterHelperModSettings.WindowResizeMinX, ref WindowResizeMinXText, 1);
            listingStandard.Gap();

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
    }
}
