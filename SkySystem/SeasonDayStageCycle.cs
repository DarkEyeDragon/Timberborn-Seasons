using Bindito.Core;
using Seasons.Seasons;
using Timberborn.SkySystem;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Seasons.SkySystem;

public class SeasonDayStageCycle : DayStageCycle
{
    /*private SeasonService _seasonService;

    [Inject]
    public void InjectDependencies(
        IDayNightCycle dayNightCycle,
        SeasonService seasonService)
    {
        _dayNightCycle = dayNightCycle;
        _seasonService = seasonService;
    }

    public new DayStageTransition GetCurrentTransition() => _dayNightCycle.FluidTimeOfDay != TimeOfDay.Daytime
        ? Transition(TimeOfDay.Nighttime, TimeOfDay.Daytime, DayStage.Sunset, DayStage.Night, DayStage.Sunrise)
        : Transition(TimeOfDay.Daytime, TimeOfDay.Nighttime, DayStage.Sunrise, DayStage.Day, DayStage.Sunset);

    private new DayStageTransition Transition(
        TimeOfDay currentTimeOfDay,
        TimeOfDay nextTimeOfDay,
        DayStage dayStage1,
        DayStage dayStage2,
        DayStage dayStage3)
    {
        float num = 24f - _dayNightCycle.FluidHoursToNextStartOf(currentTimeOfDay);
        float nextStartOf = _dayNightCycle.FluidHoursToNextStartOf(nextTimeOfDay);
        return num >= _sunriseSunsetLengthInHours
            ? Transition(dayStage2, dayStage3, nextStartOf)
            : Transition(dayStage1, dayStage2, _sunriseSunsetLengthInHours - num);
    }

    private new DayStageTransition Transition(
        DayStage currentDayStage,
        DayStage nextDayStage,
        float hoursToNextDayStage)
    {
        float transitionProgress =
            Mathf.SmoothStep(0.0f, 1f, 1f - Mathf.Clamp01(hoursToNextDayStage / _transitionLengthInHours));
        //TODO rework hardcoded values
        bool isDrought = _seasonService.CurrentSeason.SeasonType.IsDifficult;
        //TODO implement transition for each season
        //bool nextDayStageIsInDrought = nextDayStage == DayStage.Sunrise ? _weatherService.NextDayIsInDrought() : isDrought;
        return new DayStageTransition(currentDayStage, isDrought, nextDayStage, false, transitionProgress);
    }*/
}