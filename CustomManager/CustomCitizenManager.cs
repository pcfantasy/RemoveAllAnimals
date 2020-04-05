using ColossalFramework;
using ColossalFramework.Math;
using Harmony;
using RemoveAllAnimals.Util;
using UnityEngine;

namespace RemoveAllAnimals.CustomManager
{
    public class CustomCitizenManager
    {
        public static bool CitizenManagerCreateCitizenInstancePrefix(CitizenInfo info, ref bool __result)
        {
            // NON-STOCK CODE START
            if (RemoveAllAnimals.removeAnimal)
            {
                CitizenAI AI = info.m_citizenAI as CitizenAI;
                if (AI.IsAnimal() && !(info.m_citizenAI is RescueAnimalAI))
                {
                    __result = false;
                    return false;
                }
            }
            return true;
        }
    }
}
