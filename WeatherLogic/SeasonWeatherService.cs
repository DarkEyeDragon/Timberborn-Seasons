using Seasons.Growing;
using Seasons.Seasons;
using Timberborn.Core;
using Timberborn.Debugging;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using Timberborn.TimeSystem;

namespace Seasons.WeatherLogic;

public class SeasonWeatherService : ILoadableSingleton
{
    private static readonly SingletonKey WeatherServiceKey = new(nameof(SeasonWeatherService));

    private readonly EventBus _eventBus;
    private readonly SeasonService _seasonService;
    private readonly MapEditorMode _mapEditorMode;
    private readonly SeasonCycleTrackerService _seasonCycleTrackerService;
    private readonly PlantableService _plantableService;
    private readonly TerrainMeshManager _meshManager;

    public SeasonWeatherService(
        EventBus eventBus,
        SeasonService seasonService,
        MapEditorMode mapEditorMode,
        SeasonCycleTrackerService seasonCycleTrackerService,
        PlantableService plantableService)
    {
        _eventBus = eventBus;
        _seasonService = seasonService;
        _mapEditorMode = mapEditorMode;
        _seasonCycleTrackerService = seasonCycleTrackerService;
        _plantableService = plantableService;
    }

    public void Load()
    {
        _eventBus.Register(this);
    }

    [OnEvent]
    public void OnDaytimeStart(DaytimeStartEvent daytimeStartEvent) => StartNextDay();

    //TODO dont rely on SeasonWeatherService to start next season
    private void StartNextDay()
    {
        _seasonCycleTrackerService.Day++;
        var currentSeason = _seasonService.CurrentSeason;
        //TODO growing logic
        if (currentSeason.CurrentDay.Temperature > 0)
        {
            SeasonsPlugin.ConsoleWriter.LogInfo("Resume Growth");
            _plantableService.ResumeGrowth();
        }
        else
        {
            SeasonsPlugin.ConsoleWriter.LogInfo("Pause Growth");
            _plantableService.PauseGrowth();
        }

        if (_seasonCycleTrackerService.Day == currentSeason.TotalDays)
        {
            if (currentSeason.SeasonType.Order == _seasonService.SeasonTypes.Count - 1)
            {
                _seasonCycleTrackerService.Cycle++;
                _seasonService.NewCycle();
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
        }
    }

    public ConsoleModuleDefinition GetDefinition() => new ConsoleModuleDefinition.Builder().AddMethod(
        ConsoleMethod.Create(
            "Next Season",
            () => { _seasonService.NextSeason(); })
    ).Build();
}