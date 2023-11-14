using HarmonyLib;
using Timberborn.Growing;
using Timberborn.TimeSystem;

/*namespace FloodSeason.Patches.NaturalResource;

[HarmonyPatch]
public class GrowablePatch
{
    [HarmonyPatch(typeof(Growable), nameof(Growable.ResumeGrowing))]
    [HarmonyPostfix]
    static void ResumeGrowing(ref LivingNaturalResource ____livingNaturalResource, ref ITimeTrigger ____timeTrigger)
    {
        var seasonService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();
        if (seasonService.CurrentSeason.CurrentDay.Temperature >=0)
        {
            ____timeTrigger.Pause();
        }
    }
}*/