using System;
using FloodSeason.Seasons.Types;
using FloodSeason.Weather.Modifiers;
using Timberborn.Core;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.SkySystem;
using Timberborn.TimeSystem;
using Timberborn.WeatherSystem;
using UnityEngine;

namespace FloodSeason.Weather;

public class SeasonCycleTrackerService : ISaveableSingleton, ILoadableSingleton
{
    private static readonly SingletonKey SeasonCycleTrackerKey = new(nameof(SeasonCycleTrackerService));
    private static readonly PropertyKey<int> YearKey = new(nameof(Year));
    private static readonly PropertyKey<int> MonthKey = new(nameof(Month));
    private static readonly PropertyKey<int> DayKey = new(nameof(Day));

    private readonly WeatherDurationService _weatherDurationService;
    private readonly MapEditorMode _mapEditorMode;
    private readonly ISingletonLoader _singletonLoader;
    private readonly IDayNightCycle _dayNightCycle;

    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public int TotalCycles => _dayNightCycle.DayNumber;

    public SeasonCycleTrackerService(WeatherDurationService weatherDurationService, MapEditorMode _mapEditorMode,
        ISingletonLoader singletonLoader, IDayNightCycle dayNightCycle)
    {
        _weatherDurationService = weatherDurationService;
        this._mapEditorMode = _mapEditorMode;
        _singletonLoader = singletonLoader;
        _dayNightCycle = dayNightCycle;
    }

    //TODO save state of weather
    public void Save(ISingletonSaver singletonSaver)
    {
        if (_mapEditorMode.IsMapEditor)
            return;
        IObjectSaver singleton = singletonSaver.GetSingleton(SeasonCycleTrackerKey);
        singleton.Set(YearKey, Year);
        singleton.Set(MonthKey, Month);
        singleton.Set(DayKey, Day);
    }

    public void Load()
    {
        SingletonKey key = new SingletonKey("WeatherCycleService");
        IObjectLoader objectLoader = _singletonLoader.HasSingleton(SeasonCycleTrackerKey)
            ? _singletonLoader.GetSingleton(SeasonCycleTrackerKey)
            : _singletonLoader.HasSingleton(key)
                ? _singletonLoader.GetSingleton(key)
                : null;
        if (objectLoader != null)
        {
            SeasonsPlugin.ConsoleWriter.LogInfo("ObjectLoader != null");
            Year = Math.Max(objectLoader.Get(YearKey), 0);
            Month = objectLoader.Get(MonthKey);
            Day = objectLoader.Get(DayKey);
        }
        else
        {
            Year = 1;
            Month = 1;
            Day = 1;
        }
    }

    public int GenerateWeatherDuration(IModifier modifier)
    {
        var multiplier = _weatherDurationService.DroughtDurationHandicapMultiplier(TotalCycles);
        int duration;
        if (modifier.IsNegative)
        {
            duration = _weatherDurationService.RoundedRandomRange(
                multiplier * _weatherDurationService._minDroughtDuration,
                multiplier * _weatherDurationService._maxDroughtDuration);
        }
        else
        {
            duration = _weatherDurationService.RoundedRandomRange(
                multiplier * _weatherDurationService._minTemperateWeatherDuration,
                multiplier * _weatherDurationService._maxTemperateWeatherDuration);
        }

        return duration;
    }
}