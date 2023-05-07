using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FloodSeason.Events;
using FloodSeason.Seasons;
using FloodSeason.Seasons.Types;
using FloodSeason.WeatherLogic;
using FloodSeason.WeatherLogic.Modifiers;
using Timberborn.Common;
using Timberborn.Core;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WeatherSystem;
using UnityEngine;

/*namespace FloodSeason.Weather;

public class ForecastService : ISaveableSingleton, ILoadableSingleton
{
    private static readonly SingletonKey ForeServiceKey = new(nameof(ForecastService));

    
    private readonly IRandomNumberGenerator _randomNumberGenerator;
    private readonly EventBus _eventBus;
    private readonly WeatherDurationService _weatherDurationService;
    private readonly SeasonCycleTrackerService _seasonCycleTrackerService;
    private readonly MapEditorMode _mapEditorMode;
    private Queue<WeatherLogic.Weather> _forecast = new(4);

    public WeatherInstance CurrentWeather { get; private set; }
    public WeatherInstance PreviousWeather { get; private set; }

    public bool Finished { get; private set; }

    public ForecastService(IRandomNumberGenerator randomNumberGenerator, EventBus eventBus,
        WeatherDurationService weatherDurationService, SeasonCycleTrackerService seasonCycleTrackerService,
        MapEditorMode mapEditorMode)
    {
        _randomNumberGenerator = randomNumberGenerator;
        _eventBus = eventBus;
        _weatherDurationService = weatherDurationService;
        _seasonCycleTrackerService = seasonCycleTrackerService;
        _mapEditorMode = mapEditorMode;
    }

    public void Initialize(Season season)
    {
        GenerateForecast(season);
        NextForecast();
    }

    public void NextDay()
    {
        if (CurrentWeather.Progress == CurrentWeather.Duration - 1)
        {
            NextForecast();
        }
        else
        {
            CurrentWeather.NextDay();
        }
    }

    private void NextForecast()
    {
        PreviousWeather = CurrentWeather;
        CurrentWeather = _forecast.Dequeue();
    }

    /// <summary>
    /// Generate a forecast for an entire season.
    /// </summary>
    /// <param name="season">The season to generate the forcast for</param>
    private void GenerateForecast(Season season)
    {
        var modifiers = season.Modifiers.OfType<WaterSourceModifier>().ToArray();
        var totalDuration = 0;
        var forecast = new List<WeatherLogic.Weather>();
        if (_seasonCycleTrackerService.TotalCycles <= _weatherDurationService._handicapCycles)
        {
            forecast.Add(new WeatherInstance(WeatherType.Sun, _seasonCycleTrackerService.TotalCycles,
                _weatherDurationService._handicapCycles, season.TemperatureRange, 1, false));
            totalDuration += _weatherDurationService._handicapCycles;
        }

        while (totalDuration < Season.TotalDuration)
        {
            _randomNumberGenerator.Shuffle(modifiers);
            foreach (var waterSourceModifier in modifiers)
            {
                var random = _randomNumberGenerator.Range(0f, 1f);
                if (random < waterSourceModifier.Weight)
                {
                    var duration = _seasonCycleTrackerService.GenerateWeatherDuration(waterSourceModifier);
                    if (totalDuration + duration < Season.TotalDuration)
                    {
                        totalDuration += duration;
                    }
                    else
                    {
                        duration = Season.TotalDuration - totalDuration;
                        totalDuration = Season.TotalDuration;
                    }

                    //totalDuration += duration;
                    var instance =
                        new WeatherInstance(waterSourceModifier.WeatherType, totalDuration - duration, duration,
                            season.TemperatureRange, waterSourceModifier.Multiplier, waterSourceModifier.IsNegative);
                    forecast.Add(instance);
                    _eventBus.Post(new WeatherChangedEvent(instance));
                    SeasonsPlugin.ConsoleWriter.LogInfo(
                        $"Forecast Weather: {instance.WeatherType} for {duration} days (x{waterSourceModifier.Multiplier})");
                }
            }
        }

        _forecast = Merge(forecast);
    }

    private Queue<WeatherInstance> Merge(IList<WeatherInstance> weatherInstances)
    {
        WeatherInstance previous = null;
        var forecast = new List<WeatherInstance>();
        foreach (var weatherInstance in weatherInstances)
        {
            if (previous == null)
            {
                previous = weatherInstance;
                continue;
            }

            if (previous.WeatherType == weatherInstance.WeatherType && !weatherInstance.IsNegative)
            {
                previous = new WeatherInstance(previous.WeatherType, previous.StartDay,
                    previous.Duration + weatherInstance.Duration, previous.Temperature, previous.StrengthMultiplier,
                    weatherInstance.IsNegative);
            }
            else
            {
                forecast.Add(previous);
                previous = weatherInstance;
            }
        }

        if (previous != null) forecast.Add(previous);
        foreach (var instance in forecast)
        {
            SeasonsPlugin.ConsoleWriter.LogInfo(
                $"Forecast Merged Weather: {instance.WeatherType} for {instance.Duration} days (x{instance.StrengthMultiplier})");
        }

        return new Queue<WeatherInstance>(forecast);
    }

    public void Save(ISingletonSaver singletonSaver)
    {
        if (_mapEditorMode.IsMapEditor)
            return;
        IObjectSaver singleton = singletonSaver.GetSingleton(ForeServiceKey);
        singleton.Set();
    }

    public void Load()
    {
        throw new NotImplementedException();
    }
}*/