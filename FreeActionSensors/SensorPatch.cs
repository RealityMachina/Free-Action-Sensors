using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using BattleTech;
using UnityEngine;
using System.Reflection;

namespace FreeActionSensors
{
    public class SensorPatch
    {
        public static void Init()
        {
            var harmony = HarmonyInstance.Create("Battletech.realitymachina.FreeSensorLock");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(BattleTech.SensorLockSequence))]
    [HarmonyPatch("ConsumesFiring", PropertyMethod.Getter)]
    public static class BattleTech_SensorLockConsumesFiring_Prefix
    {
        static bool Prefix(SensorLockSequence __instance, ref bool __result)
        {
            if (__instance == null)
            {
                throw new ArgumentNullException(nameof(__instance));
            }

            __result = false;
            return false; //override
        }
    }

    [HarmonyPatch(typeof(BattleTech.SensorLockSequence))]
    [HarmonyPatch("ConsumesFiring", PropertyMethod.Getter)]
    public static class BattleTech_SensorLockConsumesFiring_Postfix
    {
        static void Postfix(SensorLockSequence __instance, ref bool __result)
        {
            if (__instance == null)
            {
                throw new ArgumentNullException(nameof(__instance));
            }

            if(__result)
            {
                __result = false;
            }
        }
    }

}
