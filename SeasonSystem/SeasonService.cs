using System.Collections.Generic;
using System.Linq;
using Seasons.Calender;
using Seasons.Events;
using Seasons.Exceptions;
using Seasons.Extensions;
using Seasons.Seasons.Types;
using Seasons.SeasonSystem.Types;
using Seasons.WeatherLogic;
using Seasons.WeatherLogic.Modifiers;
using Timberborn.Common;
using Timberborn.Core;
using Timberborn.HazardousWeatherSystem;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.WeatherSystem;

namespace Seasons.SeasonSystem;

public class SeasonService : ISaveableSingleton, ILoadableSingleton
{
    private static readonly SingletonKey SeasonServiceKey = new SingletonKey(nameof(SeasonService));
    private static readonly PropertyKey<Season> CurrentSeasonKey = new PropertyKey<Season>(nameof(CurrentSeason));
    public Season CurrentSeason { get; set; }
    public Season PreviousSeason { get; private set; }
    private readonly ISingletonLoader _singletonLoader;
    private readonly EventBus _eventBus;
    private readonly MapEditorMode _mapEditorMode;
    private readonly IRandomNumberGenerator _randomNumberGenerator;
    private readonly TemperateWeatherDurationService _weatherDurationService;
    private readonly SeasonCycleTrackerService _seasonCycleTrackerService;

    public bool IsActive { get; set; } = false;
    public Dictionary<int, SeasonType> SeasonTypes { get; private set; }

    //TODO return immutable collection
    //public List<Season> Seasons => _seasons.ToList();
    //private Texture _defaultTexture;
    private int _seasonIndex;

    [OnEvent]
    public void OnDaytimeStart(DaytimeStartEvent daytimeStartEvent)
    {
        NextDay();
    }


    public SeasonService(ISingletonLoader singletonLoader, EventBus eventBus, MapEditorMode mapEditorMode,
        IRandomNumberGenerator randomNumberGenerator, TemperateWeatherDurationService weatherDurationService,
        SeasonCycleTrackerService seasonCycleTrackerService)
    {
        SeasonTypes = new Dictionary<int, SeasonType>();
        //TODO properly register seasons
        Register(new Spring());
        Register(new Summer());
        Register(new Autumn());
        Register(new Winter());
        _singletonLoader = singletonLoader;
        _eventBus = eventBus;
        _mapEditorMode = mapEditorMode;
        _randomNumberGenerator = randomNumberGenerator;
        _weatherDurationService = weatherDurationService;
        _seasonCycleTrackerService = seasonCycleTrackerService;
    }

    public void Register(SeasonType seasonType)
    {
        if (IsActive)
        {
            throw new InvalidStateException("Cannot register new seasons while the season manager is running");
        }

        SeasonTypes.Add(seasonType.Order, seasonType);
    }

    public void Unregister(SeasonType seasonType)
    {
        if (IsActive)
        {
            throw new InvalidStateException("Cannot unregister seasons while the season manager is running");
        }

        SeasonTypes.Remove(seasonType.Order);
    }

    
    
    public void NextSeason()
    {
        var index = _seasonIndex++;
        if (index >= SeasonTypes.Count || index < 0)
        {
            throw new InvalidStateException(
                $"Attempting to load season out of index ({index}) Max {SeasonTypes.Count}");
        }

        var seasonType = SeasonTypes[index];
        int duration = _weatherDurationService.GenerateDuration();
        SeasonsPlugin.ConsoleWriter.LogInfo($"SeasonTypes: {SeasonTypes.Count}");
        SeasonsPlugin.ConsoleWriter.LogInfo($"Duration: {duration}");
        //TODO remove /4
        //var length = seasonType.IsDifficult ? drought : temperate/4;

        var forecast = GenerateInitialForecast(seasonType, duration);
        
        //TODO improve system
        SeasonsPlugin.ConsoleWriter.LogInfo($"Forecast size: {forecast.Count}");
        PreviousSeason = CurrentSeason;
        CurrentSeason = new Season(SeasonTypes[index]);
        SeasonsPlugin.ConsoleWriter.LogInfo($"Current Season: {CurrentSeason.SeasonType.Name}");
        CurrentSeason.SetForecast(forecast);
        //CurrentSeason =  SeasonTypes[index];
        _eventBus.Post(new SeasonChangedEvent(CurrentSeason));
        //_eventBus.Post(new HazardousWeatherStartedEvent(new BadtideWeather()));
    }

    public void NewYear()
    {
        _seasonIndex = 0;
        NextSeason();
    }

    public void Save(ISingletonSaver singletonSaver)
    {
        if (_mapEditorMode.IsMapEditor)
            return;
        IObjectSaver singleton = singletonSaver.GetSingleton(SeasonServiceKey);
        singleton.Set(CurrentSeasonKey, CurrentSeason, new SeasonObjectSerializer());
        //singleton.Set(, CurrentSeason.);*/
    }

    public void Load()
    {
        if (!_singletonLoader.HasSingleton(SeasonServiceKey))
        {
            NextSeason();
        }
        else
        {
            _singletonLoader.GetSingleton(SeasonServiceKey).Get(CurrentSeasonKey, new SeasonObjectSerializer());
        }
        _eventBus.Register(this);
    }

    public void NextDay()
    {
        if (CurrentSeason.RemainingDays.Count > 0)
        {
            CurrentSeason.NextDay();
        }
        else
        {
            if (CurrentSeason.SeasonType.Order != SeasonTypes.Count - 1)
            {
                NextSeason();
            }
            else
            {
                NewYear();
            }
        }
    }

    private Day GenerateNextForecastDay(SeasonType seasonType)
    {
        var modifiers = seasonType.Modifiers.OfType<WaterSourceModifier>().ToArray();
        var ordered = modifiers.OrderBy(modifier => modifier.Multiplier);
        WaterSourceModifier modifier = null;
        while (modifier is null)
        {
            foreach (var waterSourceModifier in ordered)
            {
                if (_randomNumberGenerator.Range(0f, 1f) >= waterSourceModifier.Weight)
                {
                    modifier = waterSourceModifier;
                    break;
                }
            }
        }

        return new Day
        {
            Temperature = _randomNumberGenerator.Range(seasonType.TemperatureRange),
            Modifier = modifier
        };
    }

    private Queue<Day> GenerateInitialForecast(SeasonType seasonType, int amount)
    {
        var queue = new Queue<Day>();
        for (int i = 0; i < amount; i++)
        {
            queue.Enqueue(GenerateNextForecastDay(seasonType));
        }

        return queue;
    }
}