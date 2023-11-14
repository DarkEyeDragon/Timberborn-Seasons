using System;
using Seasons.Seasons;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TickSystem;
using Timberborn.TimeSystem;

namespace Seasons.Temperature;

public class TemperatureService : ITickableSingleton, ILoadableSingleton, ISaveableSingleton
{
    private static readonly SingletonKey TemperatureServiceKey = new SingletonKey(nameof(TemperatureService));
    private static readonly PropertyKey<float> CurrentTemperatureKey = new PropertyKey<float>(nameof(CurrentTemperature));

    private readonly SeasonService _seasonService;
    private readonly IDayNightCycle _dayNightCycle;
    private readonly ISingletonLoader _singletonLoader;
    private const int MaxTicks = 10;

    private int _index = 0;

    public TemperatureService(SeasonService seasonService, IDayNightCycle dayNightCycle, ISingletonLoader singletonLoader)
    {
        _seasonService = seasonService;
        _dayNightCycle = dayNightCycle;
        _singletonLoader = singletonLoader;
    }

    public double CurrentTemperature { get; protected set; }

    public void CalculateCurrentTemperature()
    {
        var delta = _dayNightCycle.DayProgress;
        //double u = 0.4;
        //double y = 1 / (u * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -(1 / 2) * Math.Pow(delta-Offset / (2*Deviation), 2));
        int min = _seasonService.CurrentSeason.SeasonType.TemperatureRange.Min;
        int max = _seasonService.CurrentSeason.SeasonType.TemperatureRange.Max;
        double temp = min + Math.Sin(delta / Math.PI * 0.1) * (min - max);
        CurrentTemperature = temp;
        SeasonsPlugin.ConsoleWriter.LogInfo($"Temp:{CurrentTemperature} delta:{delta} y:{temp}");
    }

    public void Tick()
    {
        if (_index++ >= MaxTicks)
        {
            _index = 0;
            CalculateCurrentTemperature();
        }
    }

    public void Load()
    {
        if (_singletonLoader.HasSingleton(TemperatureServiceKey))
        {
            CurrentTemperature = _singletonLoader.GetSingleton(TemperatureServiceKey).Get(CurrentTemperatureKey);
        }
        else
        {
            CalculateCurrentTemperature();
        }
    }

    public void Save(ISingletonSaver singletonSaver)
    {
        singletonSaver.GetSingleton(TemperatureServiceKey).Set(CurrentTemperatureKey, (float)CurrentTemperature);
    }
}