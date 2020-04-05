using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.IO;
using ColossalFramework;
using System.Reflection;
using System;
using System.Collections.Generic;
using ColossalFramework.Plugins;
using ColossalFramework.Math;
using RemoveAllAnimals.Util;
using RemoveAllAnimals.CustomManager;

namespace RemoveAllAnimals
{
    public class Loader : LoadingExtensionBase
    {
        public static bool DetourInited = false;
        public static bool HarmonyDetourInited = false;
        public static LoadMode CurrentLoadMode;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            CurrentLoadMode = mode;
            if (RemoveAllAnimals.IsEnabled)
            {
                if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
                {
                    InitDetour();
                    HarmonyInitDetour();
                    RemoveAllAnimals.LoadSetting();
                    DebugLog.LogToFileOnly("OnLevelLoaded");
                    if (mode == LoadMode.NewGame)
                    {
                        DebugLog.LogToFileOnly("InitData");
                    }
                }
            }
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            if (CurrentLoadMode == LoadMode.LoadGame || CurrentLoadMode == LoadMode.NewGame)
            {
                if (RemoveAllAnimals.IsEnabled && DetourInited)
                {
                    RevertDetours();
                    HarmonyRevertDetour();
                }
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();
        }

        public void HarmonyInitDetour()
        {
            if (!HarmonyDetourInited)
            {
                DebugLog.LogToFileOnly("Init harmony detours");
                HarmonyDetours.Apply();
            }
        }

        public void HarmonyRevertDetour()
        {
            if (HarmonyDetourInited)
            {
                DebugLog.LogToFileOnly("Revert harmony detours");
                HarmonyDetours.DeApply();
            }
        }

        public void InitDetour()
        {
            DetourInited = true;
        }

        public void RevertDetours()
        {
            DetourInited = false;
        }
    }
}
