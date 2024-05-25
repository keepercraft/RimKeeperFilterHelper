using HarmonyLib;
using Keepercraft.RimKeeperFilterHelper.Models;
using RimWorld;
using System.Linq;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper.Extensions
{
    public static class FilterExtension
    {
        public static void SetFilter(this StorageSettings storage, ITab_Storage_Filter_Model model, bool allow = true, bool copy = false)
        {
            if (copy)
            {
                storage.filter.SetDisallowAll();
                storage.filter.AllowedHitPointsPercents = model.AllowedHitPointsPercents;
                storage.filter.AllowedMentalBreakChance = model.AllowedMentalBreakChance;
                storage.filter.AllowedQualityLevels = model.AllowedQualityLevels;
                //context.filter.OnlySpecialFilters = model.OnlySpecialFilters;
            }

            model.Allowed
                .Select(s => DefDatabase<ThingDef>.GetNamed(s, false))
                .Where(s => s != null)
                .Do(c => storage.filter.SetAllow(c, allow));
            model.DisallowedSpecialFilters
                .Select(s => DefDatabase<SpecialThingFilterDef>.GetNamed(s, false))
                .Where(s => s != null)
                .Do(c => storage.filter.SetAllow(c, allow));     
        }

        public static ITab_Storage_Filter_Model StoreFilter(this StorageSettings storage, string name)
        {
            ITab_Storage_Filter_Model model = new ITab_Storage_Filter_Model()
            {
                AllowedHitPointsPercents = storage.filter.AllowedHitPointsPercents,
                AllowedMentalBreakChance = storage.filter.AllowedMentalBreakChance,
                AllowedQualityLevels = storage.filter.AllowedQualityLevels,
                OnlySpecialFilters = storage.filter.OnlySpecialFilters,
                Name = name
            };
            storage.filter.AllowedThingDefs.Do(s => model.Allowed.Add(s.defName));
            storage.filter.hiddenSpecialFilters.Do(s => model.DisallowedSpecialFilters.Add(s.defName));
            return model;
        }
    }
}
