using System;
using Seasons.Growing;
using Seasons.Seasons;
using Seasons.SeasonSystem;
using Seasons.WeatherLogic;
using Timberborn.Common;
using Timberborn.Core;
using Timberborn.Debugging;
using Timberborn.HazardousWeatherSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TimeSystem;
using Timberborn.WeatherSystem;

namespace Seasons.WeatherSystem;

public class SeasonWeatherService : ILoadableSingleton
{
    private static readonly SingletonKey WeatherServiceKey = new(nameof(SeasonWeatherService));

    private readonly SeasonService _seasonService;
    private readonly SeasonCycleTrackerService _seasonCycleTrackerService;
    private readonly PlantableService _plantableService;

    private readonly EventBus _eventBus;
    private readonly DroughtWeather _droughtWeather;
    private readonly IRandomNumberGenerator _numberGenerator;

    private readonly WeatherService _weatherService;
    //private readonly TerrainMeshManager _meshManager;

    public SeasonWeatherService(SeasonService seasonService,
        SeasonCycleTrackerService seasonCycleTrackerService,
        PlantableService plantableService,
        //TerrainMeshManager terrainMeshManager,
        EventBus eventBus, ISingletonLoader singletonLoader,
        TemperateWeatherDurationService temperateWeatherDurationService,
        IDayNightCycle dayNightCycle,
        MapEditorMode mapEditorMode,
        DroughtWeather droughtWeather,
        IRandomNumberGenerator numberGenerator,
        HazardousWeatherService hazardousWeatherService)
    {
        _seasonService = seasonService;
        _seasonCycleTrackerService = seasonCycleTrackerService;
        _plantableService = plantableService;
        //_meshManager = terrainMeshManager;
        _eventBus = eventBus;
        _droughtWeather = droughtWeather;
        _numberGenerator = numberGenerator;
    }

    public new void Load()
    {
        _eventBus.Register(this);
    }

    [OnEvent]
    public void OnDaytimeStart(DaytimeStartEvent daytimeStartEvent) => StartNextDay();

    //TODO dont rely on SeasonWeatherService to start next season
    public void StartNextDay()
    {
        //_seasonCycleTrackerService.Day++;
        var currentSeason = _seasonService.CurrentSeason;
        //TODO growing logic
        /*if (currentSeason.CurrentDay.Temperature > 0)
        {
            SeasonsPlugin.ConsoleWriter.LogInfo("Resume Growth");
            _plantableService.ResumeGrowth();
        }
        else
        {
            SeasonsPlugin.ConsoleWriter.LogInfo("Pause Growth");
            _plantableService.PauseGrowth();
        }*/

        /*if (_seasonCycleTrackerService.Day == currentSeason.TotalDays)
        {
            if (currentSeason.SeasonType.Order == _seasonService.SeasonTypes.Count - 1)
            {
                _seasonCycleTrackerService.Cycle++;
                _seasonService.NewYear();
            }
            else
            {
                _seasonService.NextSeason();
            }

            _seasonCycleTrackerService.Day = 1;
        }
        else
        {
            _seasonService.NextDay();
        }*/
    }

    public void GenerateSeasonalWeather()
    {
        int maxDuration = Math.Max(_seasonService.CurrentSeason.RemainingDays.Count,
            _droughtWeather.GetDurationAtCycle(_seasonCycleTrackerService.TotalCycles));
        var seasonType = _seasonService.CurrentSeason.SeasonType;
        var modifiers = seasonType.Modifiers;
        var length = _numberGenerator.Range(1, maxDuration + 1);
        int duration = 0;
        while (duration < maxDuration)
        {
            duration = Math.Min(duration + length, maxDuration);
        }
    }
}