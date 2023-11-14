using HarmonyLib;
using Seasons.Seasons;
using Timberborn.SkySystem;
using UnityEngine;

namespace Seasons.Patches;

[HarmonyPatch(typeof(DayStageCycle), nameof(DayStageCycle.Transition), typeof(DayStage), typeof(DayStage), typeof(float))]
public class DayStageCyclePatch
{
    static void Postfix(DayStage currentDayStage,
        DayStage nextDayStage,
        float hoursToNextDayStage, ref DayStageTransition __result, DayStageCycle __instance)
    {
        var seasonService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonService>();
        float transitionProgress = Mathf.SmoothStep(0.0f, 1f, 1f - Mathf.Clamp01(hoursToNextDayStage / __instance._transitionLengthInHours));
        //TODO rework
        bool isDrought = seasonService.CurrentSeason.SeasonType.Name.Equals("Summer");
        __result = new DayStageTransition(currentDayStage, isDrought, nextDayStage, false, transitionProgress);
    }
}