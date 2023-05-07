using FloodSeason.WeatherLogic.Modifiers;
using Timberborn.Core;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.WeatherSystem;

namespace FloodSeason.WeatherLogic;

public class SeasonCycleTrackerService : ISaveableSingleton, ILoadableSingleton
{
    private static readonly SingletonKey SeasonCycleTrackerKey = new(nameof(SeasonCycleTrackerService));
    private static readonly PropertyKey<int> DayKey = new(nameof(Day));
    private static readonly PropertyKey<int> CycleKey = new(nameof(Cycle));

    private readonly MapEditorMode _mapEditorMode;
    private readonly ISingletonLoader _singletonLoader;
    private readonly IDayNightCycle _dayNightCycle;

    public int Day { get; set; }
    public int Cycle { get; set; }
    public int TotalCycles => _dayNightCycle.DayNumber;

    public SeasonCycleTrackerService(WeatherDurationService weatherDurationService, MapEditorMode _mapEditorMode,
        ISingletonLoader singletonLoader, IDayNightCycle dayNightCycle)
    {
        this._mapEditorMode = _mapEditorMode;
        _singletonLoader = singletonLoader;
        _dayNightCycle = dayNightCycle;
    }
    public void Save(ISingletonSaver singletonSaver)
    {
        if (_mapEditorMode.IsMapEditor)
            return;
        IObjectSaver singleton = singletonSaver.GetSingleton(SeasonCycleTrackerKey);
        singleton.Set(DayKey, Day);
        singleton.Set(CycleKey, Cycle);
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
            Day = objectLoader.Get(DayKey);
            Cycle = objectLoader.Get(CycleKey);
        }
        else
        {
            Day = 1;
            Cycle = 1;
        }
    }
}