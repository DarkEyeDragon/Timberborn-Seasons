using FloodSeason.Seasons;
using HarmonyLib;
using Timberborn.NaturalResourcesMoisture;

namespace FloodSeason.Patches.NaturalResource;

[HarmonyPatch(typeof(WateredNaturalResource), nameof(WateredNaturalResource.StartDryingOut))]
public class WateredNaturalResourcePatch
{
    static bool Prefix()
    {
        SeasonsPlugin.ConsoleWriter.LogInfo($"Triggered {nameof(WateredNaturalResourcePatch)}");
        var seasonService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();
        //TODO implement min growing temp for plants
        //Skip reset timer if freezing since we dont want to resume growing yet.
        return seasonService.CurrentSeason.CurrentDay.Temperature > 0;
    }
}