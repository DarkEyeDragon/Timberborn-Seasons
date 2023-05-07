﻿using System;
using FloodSeason.Seasons;
using FloodSeason.WeatherLogic;
using Timberborn.CoreUI;
using Timberborn.GameUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace FloodSeason.UI;

public class SeasonDatePanel : ILoadableSingleton, IUpdatableSingleton
{
    private static readonly string WeatherDroughtLocKey = "Weather.Drought";
    private static readonly string WeatherTemperateLocKey = "Weather.Temperate";
    private readonly GameLayout _gameLayout;
    private readonly VisualElementLoader _visualElementLoader;
    private readonly SeasonCycleTrackerService _seasonCycleTrackerService;
    private readonly TimestampFormatter _timestampFormatter;
    private readonly SeasonService _seasonService;
    private readonly ILoc _loc;
    private readonly ITooltipRegistrar _tooltipRegistrar;
    private VisualElement _root;
    private Label _text;
    private string _tooltipText;

    public SeasonDatePanel(
        GameLayout gameLayout,
        VisualElementLoader visualElementLoader,
        SeasonCycleTrackerService seasonCycleTrackerService,
        TimestampFormatter timestampFormatter,
        SeasonService seasonService,
        ILoc loc,
        ITooltipRegistrar tooltipRegistrar)
    {
        _gameLayout = gameLayout;
        _visualElementLoader = visualElementLoader;
        _seasonCycleTrackerService = seasonCycleTrackerService;
        _timestampFormatter = timestampFormatter;
        _seasonService = seasonService;
        _loc = loc;
        _tooltipRegistrar = tooltipRegistrar;
    }

    public void Load()
    {
        _root = _visualElementLoader.LoadVisualElement("Master/DatePanel");
        _tooltipRegistrar.Register(_root, () => _tooltipText);
        _text = _root.Q<Label>("Text");
        _gameLayout.AddTopRight(_root, 5);
        UpdatePanel();
    }

    public void UpdateSingleton() => UpdatePanel();
    
    private void UpdatePanel()
    {
        //_root.EnableInClassList(DroughtClass, _droughtService.IsDrought);
        var seasonName = _seasonService.CurrentSeason.SeasonType.Name;
        _text.text = $"{seasonName}  {_seasonCycleTrackerService.Day} {_loc.T("seasons.cycle")} {_seasonCycleTrackerService.Cycle} ({_seasonService.CurrentSeason.CurrentDay.Temperature}°C)";
        //_tooltipText = $"{_loc.T("seasons.month")}/{_loc.T("seasons.year")}";
    }
}