using FinalIKSanity;
using HarmonyLib;
using MelonLoader;
using RootMotion.FinalIK;
using System.Linq;
using System.Reflection;

[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonInfo(typeof(FinalIKSanityMod), "Final IK Sanity", "1.0.0", "Fenrix")]

namespace FinalIKSanity
{
    public class FinalIKSanityMod : MelonMod
    {
        public override void OnApplicationStart()
        {
            HarmonyInstance.Patch(
                typeof(IKSolverHeuristic).GetMethods().Where(m => m.Name.Equals("IsValid") && m.GetParameters().Length == 1).First(),
                prefix: new HarmonyMethod(typeof(FinalIKSanityMod).GetMethod("IsValid", BindingFlags.NonPublic | BindingFlags.Static)));
        }

        private static bool IsValid(ref IKSolverHeuristic __instance, ref bool __result, ref string message)
        {
            if (__instance.maxIterations > 64)
            {
                __result = false;
                message = "The solver requested too many iterations.";

                return false;
            }

            return true;
        }
    }
}
