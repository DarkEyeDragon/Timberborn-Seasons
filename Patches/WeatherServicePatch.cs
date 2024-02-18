using HarmonyLib;
using Seasons.WeatherSystem;
using TimberApi.DependencyContainerSystem;
using Timberborn.HazardousWeatherSystem;
using Timberborn.WeatherSystem;

namespace Seasons.Patches;

[HarmonyPatch]
public class WeatherServicePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(WeatherService), nameof(WeatherService.OnDaytimeStart))]
    static bool OnDaytimeStart()
    {
        return false;
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(WeatherService), nameof(WeatherService.StartNextCycle))]
    static bool StartNextCycle()
    {
        int cycle = DependencyContainer.GetInstance<WeatherService>().Cycle;
        DependencyContainer.GetInstance<HazardousWeatherService>().SetForCycle(cycle);
        return false;
    }
}