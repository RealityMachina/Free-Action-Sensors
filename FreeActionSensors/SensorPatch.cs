using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using BattleTech;
using UnityEngine;
using System.Reflection;
using BattleTech.UI;

namespace FreeActionSensors
{
    public class SensorPatch
    {
        public static Mech sensorMech;
        public static bool justScanned;
        public static void Init()
        {
            var harmony = HarmonyInstance.Create("Battletech.realitymachina.FreeSensorLock");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(BattleTech.SensorLockSequence))]
    [HarmonyPatch("CompleteOrders")]
    public static class BattleTech_SensorPatchCompleteOrders_postfix
    {
        static void Postfix(ref SensorLockSequence __instance)
        {
            Mech mech = __instance.owningActor as Mech;

            if(mech != null)
            {
                __instance.owningActor.ForceUnitOnePhaseUp(mech.GUID, __instance.RootSequenceGUID, true);
            }
          
        }
    }
}
