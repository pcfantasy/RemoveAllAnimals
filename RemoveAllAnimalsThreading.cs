using ICities;
using System;
using System.Collections.Generic;
using RemoveAllAnimals.CustomAI;
using RemoveAllAnimals.Util;
using ColossalFramework.UI;
using System.Reflection;

namespace RemoveAllAnimals
{
    public class RemoveAllAnimalsThreading : ThreadingExtensionBase
    {
        public static bool isFirstTime = true;
        public override void OnBeforeSimulationFrame()
        {
            base.OnBeforeSimulationFrame();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (RemoveAllAnimals.IsEnabled)
                {
                    CheckDetour();
                }
            }
        }

        public void CheckDetour()
        {
            if (isFirstTime && Loader.DetourInited)
            {
                isFirstTime = false;
                DebugLog.LogToFileOnly("ThreadingExtension.OnBeforeSimulationFrame: First frame detected. Checking detours.");

                if (!Loader.HarmonyDetourInited)
                {
                    string error = "RealCity HarmonyDetourInit is failed, Send RemoveAllAnimals.txt to Author.";
                    DebugLog.LogToFileOnly(error);
                    UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage("Incompatibility Issue", error, true);
                }
            }
        }
    }
}
