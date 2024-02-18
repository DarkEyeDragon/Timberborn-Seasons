using HarmonyLib;
using Seasons.SeasonSystem;
using Seasons.SeasonSystem.Types;
using Timberborn.HazardousWeatherSystemUI;

namespace Seasons.Patches.HazardousWeatherSystemUI;

[HarmonyPatch(typeof(HazardousWeatherUIHelper), nameof(HazardousWeatherUIHelper.UpdateCurrentUISpecification))]
public static class HazardousWeatherUIHelperPatch
{
    public static bool Prefix(ref IHazardousWeatherUISpecification ____currentUISpecification)
    {
        //var service = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();
        var specification = SeasonType.WeatherUI;
        ____currentUISpecification = specification;
        return false;
    }
}