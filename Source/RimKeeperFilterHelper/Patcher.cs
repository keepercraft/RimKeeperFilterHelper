using HarmonyLib;
using Keepercraft.RimKeeperFilterHelper.Helpers;
using System.Linq;
using System.Reflection;
using Verse;

namespace Keepercraft.RimKeeperFilterHelper
{
    [StaticConstructorOnStartup]
    internal class Patcher
    {
        static Patcher()
        {
            string namespaceName = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            DebugHelper.SetHeader(namespaceName.Split('.').LastOrDefault());
            DebugHelper.Message("Patching");
            new Harmony(namespaceName).PatchAll();
        }
    }
}