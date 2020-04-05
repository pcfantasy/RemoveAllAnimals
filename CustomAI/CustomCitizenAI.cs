using ColossalFramework;
using RemoveAllAnimals.Util;
using UnityEngine;

namespace RemoveAllAnimals.CustomAI
{
    public class CustomCitizenAI
    {
        public static void CitizenAISimulationStepPostFix(ushort instanceID, ref CitizenInstance data)
        {
            if (RemoveAllAnimals.removeAnimal)
            {
                if (data.m_citizen != 0)
                {
                    Singleton<CitizenManager>.instance.ReleaseCitizen(data.m_citizen);
                }
                else
                {
                    Singleton<CitizenManager>.instance.ReleaseCitizenInstance(instanceID);
                }
            }
        }
    }
}
