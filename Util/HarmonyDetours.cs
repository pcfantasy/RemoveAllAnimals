using Harmony;
using System.Reflection;
using System;
using RemoveAllAnimals.CustomAI;
using RemoveAllAnimals.CustomManager;
using UnityEngine;
using ColossalFramework.Math;

namespace RemoveAllAnimals.Util
{
    internal static class HarmonyDetours
    {
        private static void ConditionalPatch(this HarmonyInstance harmony, MethodBase method, HarmonyMethod prefix, HarmonyMethod postfix)
        {
            var fullMethodName = string.Format("{0}.{1}", method.ReflectedType?.Name ?? "(null)", method.Name);
            if (harmony.GetPatchInfo(method)?.Owners?.Contains(harmony.Id) == true)
            {
                DebugLog.LogToFileOnly("Harmony patches already present for {0}" + fullMethodName.ToString());
            }
            else
            {
                DebugLog.LogToFileOnly("Patching {0}..." + fullMethodName.ToString());
                harmony.Patch(method, prefix, postfix);
            }
        }

        private static void ConditionalUnPatch(this HarmonyInstance harmony, MethodBase method, HarmonyMethod prefix = null, HarmonyMethod postfix = null)
        {
            var fullMethodName = string.Format("{0}.{1}", method.ReflectedType?.Name ?? "(null)", method.Name);
            if (prefix != null)
            {
                DebugLog.LogToFileOnly("UnPatching Prefix{0}..." + fullMethodName.ToString());
                harmony.Unpatch(method, HarmonyPatchType.Prefix);
            }
            if (postfix != null)
            {
                DebugLog.LogToFileOnly("UnPatching Postfix{0}..." + fullMethodName.ToString());
                harmony.Unpatch(method, HarmonyPatchType.Postfix);
            }
        }

        public static void Apply()
        {
            var harmony = HarmonyInstance.Create("RemoveAllAnimals");
            //1
            var livestockAISimulationStep = typeof(LivestockAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Vector3) }, null);
            var citizenAISimulationStepPostFix = typeof(CustomCitizenAI).GetMethod("CitizenAISimulationStepPostFix");
            harmony.ConditionalPatch(livestockAISimulationStep,
                null,
                new HarmonyMethod(citizenAISimulationStepPostFix));
            //2
            var petAISimulationStep = typeof(PetAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Vector3) }, null);
            harmony.ConditionalPatch(petAISimulationStep,
                null,
                new HarmonyMethod(citizenAISimulationStepPostFix));
            //3
            var birdAISimulationStep = typeof(BirdAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Vector3) }, null);
            harmony.ConditionalPatch(birdAISimulationStep,
                null,
                new HarmonyMethod(citizenAISimulationStepPostFix));
            //4
            var wildlifeAISimulationStep = typeof(WildlifeAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Vector3) }, null);
            harmony.ConditionalPatch(wildlifeAISimulationStep,
                null,
                new HarmonyMethod(citizenAISimulationStepPostFix));
            //5
            var CitizenManagerCreateCitizenInstance = typeof(CitizenManager).GetMethod("CreateCitizenInstance", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort).MakeByRefType(), typeof(Randomizer).MakeByRefType(), typeof(CitizenInfo), typeof(uint) }, null);
            var citizenManagerCreateCitizenInstancePrefix = typeof(CustomCitizenManager).GetMethod("CitizenManagerCreateCitizenInstancePrefix");
            harmony.ConditionalPatch(CitizenManagerCreateCitizenInstance,
                new HarmonyMethod(citizenManagerCreateCitizenInstancePrefix),
                null);
            Loader.HarmonyDetourInited = true;
            DebugLog.LogToFileOnly("Harmony patches applied");
        }

        public static void DeApply()
        {
            //1
            var harmony = HarmonyInstance.Create("RemoveAllAnimals");
            var livestockAISimulationStep = typeof(LivestockAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Vector3) }, null);
            var citizenAISimulationStepPostFix = typeof(CustomCitizenAI).GetMethod("CitizenAISimulationStepPostFix");
            harmony.ConditionalUnPatch(livestockAISimulationStep,
                null,
                new HarmonyMethod(citizenAISimulationStepPostFix));
            //2
            var petAISimulationStep = typeof(PetAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Vector3) }, null);
            harmony.ConditionalUnPatch(petAISimulationStep,
                null,
                new HarmonyMethod(citizenAISimulationStepPostFix));
            //3
            var birdAISimulationStep = typeof(BirdAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Vector3) }, null);
            harmony.ConditionalUnPatch(birdAISimulationStep,
                null,
                new HarmonyMethod(citizenAISimulationStepPostFix));
            //4
            var wildlifeAISimulationStep = typeof(WildlifeAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Vector3) }, null);
            harmony.ConditionalUnPatch(wildlifeAISimulationStep,
                null,
                new HarmonyMethod(citizenAISimulationStepPostFix));
            //5
            var CitizenManagerCreateCitizenInstance = typeof(CitizenManager).GetMethod("CreateCitizenInstance", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort).MakeByRefType(), typeof(Randomizer).MakeByRefType(), typeof(CitizenInfo), typeof(uint) }, null);
            var citizenManagerCreateCitizenInstancePrefix = typeof(CustomCitizenAI).GetMethod("CitizenManagerCreateCitizenInstancePrefix");
            harmony.ConditionalUnPatch(CitizenManagerCreateCitizenInstance,
                null,
                new HarmonyMethod(citizenManagerCreateCitizenInstancePrefix));
            Loader.HarmonyDetourInited = false;
            DebugLog.LogToFileOnly("Harmony patches DeApplied");
        }
    }
}
