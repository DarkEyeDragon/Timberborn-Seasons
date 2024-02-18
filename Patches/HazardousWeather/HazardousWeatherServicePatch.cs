using HarmonyLib;
using Timberborn.HazardousWeatherSystem;

namespace Seasons.Patches.HazardousWeather;

[HarmonyPatch(typeof(HazardousWeatherService))]
public class HazardousWeatherServicePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(HazardousWeatherService.StartHazardousWeather))]
    static bool StartHazardousWeather()
    {
        return false;
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(HazardousWeatherService.EndHazardousWeather))]
    static bool EndHazardousWeather()
    {
        return false;
    }
}