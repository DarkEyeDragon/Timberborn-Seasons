using System;
using Bindito.Core;
using Seasons.Calender;
using Seasons.Seasons;
using Seasons.SeasonSystem;
using Seasons.WeatherLogic;
using Seasons.WeatherSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;
using Timberborn.WaterSourceSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Seasons.WaterLogic;

public class SeasonWaterSourceController : TickableComponent
{
    private WaterSource _waterSource;
    private WaterSimulationSettings _waterSimulationSettings;
    private IDayNightCycle _dayNightCycle;
    private SeasonService _seasonService;
    private SeasonWeatherService _weatherService;
    private double _oldSpecifiedStrength;
    private float _changeDurationInDays;

    [Inject]
    public void InjectDependencies(
        WaterSimulationSettings waterSimulationSettings,
        SeasonService seasonService,
        SeasonWeatherService weatherService,
        IDayNightCycle dayNightCycle)
    {
        _waterSimulationSettings = waterSimulationSettings;
        _seasonService = seasonService;
        _weatherService = weatherService;
        _dayNightCycle = dayNightCycle;
    }

    public void Awake()
    {
        _waterSource = GetComponentFast<WaterSource>();
    }

    public override void Tick()
    {
        UpdateChangeDuration();
        UpdateStrength();
    }

    private void UpdateChangeDuration()
    {
        float specifiedStrength = _waterSource.SpecifiedStrength;
        if (Math.Abs(specifiedStrength - _oldSpecifiedStrength) < 0.00001)
            return;
        float currentStrength = 0.0f;
        float seconds = 0.0f;
        while (Math.Abs(currentStrength - specifiedStrength) > 0.00001)
        {
            currentStrength = CalculateNewStrength(currentStrength, specifiedStrength);
            seconds += Time.fixedDeltaTime;
        }

        _changeDurationInDays = _dayNightCycle.SecondsToHours(seconds) / 24f;
        _oldSpecifiedStrength = specifiedStrength;
    }

    private void UpdateStrength()
    {
        var season = _seasonService.CurrentSeason;
        float currentStrength = _waterSource.CurrentStrength;
        var currentWeather = _seasonService;
        //if(_weatherService.CurrentWeather.ActiveEvent)
        float targetStrength =
            _waterSource.SpecifiedStrength * currentWeather.CurrentSeason.CurrentDay.Modifier.Multiplier;
        if (Math.Abs(currentStrength - targetStrength) < 0.00001)
            return;
        _waterSource.SetCurrentStrength(CalculateNewStrength(currentStrength, targetStrength));
    }

    private float CalculateNewStrength(float currentStrength, float targetStrength)
    {
        float num1 = targetStrength - currentStrength;
        float changeBooster = 0;
        var currentDay = _seasonService.CurrentSeason.CurrentDay;
        var currentSeason = _seasonService.CurrentSeason;
        var dayIndex = _seasonService.CurrentSeason.PassedDays.Count;
        if (currentSeason.PassedDays.Count != 0)
        {
            Day previousDay;
            if (dayIndex >= 1)
            {
                previousDay = currentSeason.PassedDays[dayIndex - 1];
            }
            else
            {
                var passedDays = _seasonService.PreviousSeason.PassedDays;
                previousDay = passedDays[^2];
            }

            if (currentDay.Modifier.WeatherType == WeatherType.Flood ||
                previousDay.Modifier.WeatherType == WeatherType.Flood)
            {
                changeBooster = 0.5f;
            }
        }


        float num2 = (_waterSimulationSettings.MaxWaterSourceChangePerSecond + changeBooster) * Time.fixedDeltaTime;
        if (Math.Abs(num1) < num2)
            return targetStrength;
        float num3 = num2 * ChangeScaler(currentStrength, targetStrength) * Math.Sign(num1);
        return currentStrength + num3;
    }

    private float ChangeScaler(float currentStrength, float targetStrength)
    {
        float num = currentStrength / _waterSource.SpecifiedStrength;
        return Mathf.Lerp(_waterSimulationSettings.MinWaterSourceChangeScaler, 1f,
            targetStrength > currentStrength ? num : 1f - num);
    }
}