using Keepercraft.RimKeeperFilterHelper.Helpers;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper.Models
{
    public class ITab_Storage_Filter_Model: IExposable
    {
        public string Name = "";

        public List<string> Allowed = new List<string>();
        public List<string> DisallowedSpecialFilters = new List<string>();
        public FloatRange AllowedHitPointsPercents = default(FloatRange);
        public FloatRange AllowedMentalBreakChance = default(FloatRange);
        public QualityRange AllowedQualityLevels = default(QualityRange);
        public bool OnlySpecialFilters = false;

        public void ExposeData()
        {
            Scribe_Values.Look(ref Name, nameof(Name));
            Scribe_Collections.Look(ref Allowed, nameof(Allowed));
            Scribe_Collections.Look(ref DisallowedSpecialFilters, nameof(DisallowedSpecialFilters));
            Scribe_Values.Look(ref AllowedHitPointsPercents, nameof(AllowedHitPointsPercents), default(FloatRange), false);
            Scribe_Values.Look(ref AllowedMentalBreakChance, nameof(AllowedMentalBreakChance), default(FloatRange), false);
            Scribe_Values.Look(ref AllowedQualityLevels, nameof(AllowedQualityLevels), default(QualityRange), false);
            Scribe_Values.Look(ref OnlySpecialFilters, nameof(OnlySpecialFilters), false, false);
        }
    }
}
