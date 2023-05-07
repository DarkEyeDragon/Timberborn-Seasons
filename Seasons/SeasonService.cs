using System.Collections.Generic;
using System.Linq;
using FloodSeason.Calender;
using FloodSeason.Events;
using FloodSeason.Exceptions;
using FloodSeason.Extensions;
using FloodSeason.Seasons.Types;
using FloodSeason.WeatherLogic;
using FloodSeason.WeatherLogic.Modifiers;
using Timberborn.Common;
using Timberborn.Core;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WeatherSystem;
using UnityEngine;

namespace FloodSeason.Seasons;

public class SeasonService : ISaveableSingleton, ILoadableSingleton
{
    private static readonly SingletonKey SeasonServiceKey = new SingletonKey(nameof(SeasonService));
    private static readonly PropertyKey<int> CurrentSeasonKey = new PropertyKey<int>(nameof(CurrentSeason));

    private static readonly int DesertTextureProperty = Shader.PropertyToID("_DesertTex");
    public Season CurrentSeason { get; set; }
    public Season PreviousSeason { get; private set; }
    private readonly ISingletonLoader _singletonLoader;
    private readonly EventBus _eventBus;
    private readonly MapEditorMode _mapEditorMode;
    private readonly IRandomNumberGenerator _randomNumberGenerator;
    private readonly WeatherDurationService _weatherDurationService;
    private readonly SeasonCycleTrackerService _seasonCycleTrackerService;

    public bool IsActive { get; set; } = false;
    public Dictionary<int, SeasonType> SeasonTypes { get; private set; }

    //TODO return immutable collection
    //public List<Season> Seasons => _seasons.ToList();
    //private Texture _defaultTexture;
    private int _seasonIndex;

    public SeasonService(ISingletonLoader singletonLoader, EventBus eventBus, MapEditorMode mapEditorMode,
        IRandomNumberGenerator randomNumberGenerator, WeatherDurationService weatherDurationService,
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
            throw new InvalidStateException($"Attempting to load season out of index ({index})");
        }

        var seasonType = SeasonTypes[index];
        var (temperate, drought) = _weatherDurationService.GenerateDurations(_seasonCycleTrackerService.TotalCycles);
        //TODO remove /4
        var length = seasonType.IsDifficult ? drought : temperate/4;

        var forecast = GenerateInitialForecast(seasonType, length);
        PreviousSeason = CurrentSeason;
        CurrentSeason = new Season(_seasonIndex, forecast, seasonType);
        _eventBus.Post(new SeasonChangedEvent(CurrentSeason));
    }

    public void NewCycle()
    {
        _seasonIndex = 0;
        NextSeason();
    }

    public void Save(ISingletonSaver singletonSaver)
    {
        if (_mapEditorMode.IsMapEditor)
            return;
        /*IObjectSaver singleton = singletonSaver.GetSingleton(SeasonServiceKey);
        singleton.Set(CurrentSeasonKey, CurrentSeason.Index);
        singleton.Set(, CurrentSeason.);*/
    }

    public void Load()
    {
        if (!_singletonLoader.HasSingleton(SeasonServiceKey))
        {
            NextSeason();
            return;
        }

        /*CurrentSeason = _seasons.Find(season =>
            season.Index.Equals(_singletonLoader.GetSingleton(SeasonServiceKey).Get(CurrentSeasonKey)));
        while (CurrentSeason != _enumerator.Current)
        {
            _enumerator.MoveNext();
        }

        Update(CurrentSeason);*/
    }

    public void NextDay()
    {
        CurrentSeason.NextDay();
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