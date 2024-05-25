using System;
using UnityEngine;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper.Extensions
{
    public static class TreeThingFilterExtension
    {
        private static string filterSelector = null;

        public static void FlipAllow(this Listing_TreeThingFilter context, SpecialThingFilterDef sfDef)
        {
            ThingFilter filter = context.GetPrivateField<ThingFilter>("filter");
            bool flag = filter.Allows(sfDef);
            filter.SetAllow(sfDef, !flag);
        }

        public static void FlipAllow(this Listing_TreeThingFilter context, ThingDef sfDef)
        {
            ThingFilter filter = context.GetPrivateField<ThingFilter>("filter");
            bool flag = filter.Allows(sfDef);
            filter.SetAllow(sfDef, !flag);
        }

        public static void FlipAllow(this Listing_TreeThingFilter context, TreeNode_ThingCategory node)
        {
            ThingFilter filter = context.GetPrivateField<ThingFilter>("filter");
            MultiCheckboxState multiCheckboxState = context.AllowanceStateOf(node);
            filter.SetAllow(node.catDef, !(multiCheckboxState == MultiCheckboxState.On));
        }

        public static void FlipAllow(this Listing_TreeThingFilter context, string key, int lvl, Action<Listing_TreeThingFilter> action)
        {
            float curY = context.GetPrivateField<float>("curY");
            float labelWidth = context.GetPrivateProperty<float>("LabelWidth");
            float x = (lvl+2) * context.nestIndentWidth;
            Rect rect = new Rect(
                x, 
                curY,
                labelWidth - x, 
                context.lineHeight);

            //DebugHelper.Message("FlipAllow rect:{0} lvl:{1}", rect, lvl);
            //Widgets.DrawMenuSection(rect);

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0
                && rect.Contains(Event.current.mousePosition))
            {
                filterSelector = key;
                action(context);
                return;
            }
            if (filterSelector != null
                && filterSelector != key
                && rect.Contains(Event.current.mousePosition))
            {
                filterSelector = key;
                action(context);
                return;
            }
            if (filterSelector != null && Input.GetMouseButtonUp(0))
            {
                filterSelector = null;
            }
        }
    }
}