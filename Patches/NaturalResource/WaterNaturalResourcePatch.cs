using System.Net.NetworkInformation;
using FloodSeason.Seasons;
using HarmonyLib;
using Timberborn.NaturalResourcesMoisture;

namespace FloodSeason.Patches.NaturalResource;

[HarmonyPatch(typeof(WaterNaturalResource), nameof(WaterNaturalResource.WaterNeedsAreMet), MethodType.Getter)]
public class WaterNaturalResourcePatch
{
    static bool Prefix(ref bool __result)
    {
        var seasonService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();
        if (seasonService.CurrentSeason.CurrentDay.Temperature <= 0)
        {
            __result = false;
            return false;
        }
        return true;
    }
}