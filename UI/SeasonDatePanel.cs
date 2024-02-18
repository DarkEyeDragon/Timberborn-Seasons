using Seasons.Seasons;
using Seasons.SeasonSystem;
using Seasons.Temperature;
using Seasons.WeatherLogic;
using Timberborn.CoreUI;
using Timberborn.HazardousWeatherSystem;
using Timberborn.HazardousWeatherSystemUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;
using Timberborn.TooltipSystem;
using Timberborn.UIFormatters;
using Timberborn.UILayoutSystem;
using Timberborn.WeatherSystem;
using UnityEngine.UIElements;

namespace Seasons.UI;

public class SeasonDatePanel : ILoadableSingleton
{
    private static readonly string WeatherTemperateLocKey = "Weather.Temperate";

    private readonly UILayout _uiLayout;

    private readonly VisualElementLoader _visualElementLoader;

    private readonly WeatherService _weatherService;
    private readonly SeasonCycleTrackerService _seasonCycleTrackerService;

    private readonly TimestampFormatter _timestampFormatter;

    private readonly ILoc _loc;

    private readonly ITooltipRegistrar _tooltipRegistrar;

    private readonly EventBus _eventBus;
    private readonly SeasonService _seasonService;

    private readonly HazardousWeatherUIHelper _hazardousWeatherUIHelper;

    private VisualElement _root;

    private Label _text;

    private string _tooltipText;

    private string _currentIconClass;

    public SeasonDatePanel(UILayout uiLayout, VisualElementLoader visualElementLoader, WeatherService weatherService, SeasonCycleTrackerService seasonCycleTrackerService,
        TimestampFormatter timestampFormatter, ILoc loc, ITooltipRegistrar tooltipRegistrar, EventBus eventBus, SeasonService seasonService,
        HazardousWeatherUIHelper hazardousWeatherUIHelper)
    {
        _uiLayout = uiLayout;
        _visualElementLoader = visualElementLoader;
        _weatherService = weatherService;
        _seasonCycleTrackerService = seasonCycleTrackerService;
        _timestampFormatter = timestampFormatter;
        _loc = loc;
        _tooltipRegistrar = tooltipRegistrar;
        _eventBus = eventBus;
        _seasonService = seasonService;
        _hazardousWeatherUIHelper = hazardousWeatherUIHelper;
    }

    public void Load()
    {
        _eventBus.Register(this);
        _root = _visualElementLoader.LoadVisualElement("Game/DatePanel");
        _tooltipRegistrar.Register(_root, () => _tooltipText);
        _text = _root.Q<Label>("Text");
        _uiLayout.AddTopRight(_root, 5);
        UpdatePanel();
    }

    [OnEvent]
    public void OnHazardousWeatherStartedEvent(HazardousWeatherStartedEvent hazardousWeatherStartedEvent)
    {
        UpdatePanel();
    }

    [OnEvent]
    public void OnHazardousWeatherEndedEvent(HazardousWeatherEndedEvent hazardousWeatherEndedEvent)
    {
        UpdatePanel();
    }

    [OnEvent]
    public void OnDaytimeStart(DaytimeStartEvent daytimeStartEvent)
    {
        UpdatePanel();
    }

    private void UpdatePanel()
    {
        UpdateIcon();
        UpdateText();
    }

    private void UpdateIcon()
    {
        if (!string.IsNullOrEmpty(_currentIconClass))
        {
            _root.RemoveFromClassList(_currentIconClass);
            _currentIconClass = null;
        }

        if (_weatherService.IsHazardousWeather)
        {
            _currentIconClass = _hazardousWeatherUIHelper.IconClass;
            _root.AddToClassList(_currentIconClass);
        }
    }

    private void UpdateText()
    {
        _text.text = _timestampFormatter.FormatLongLocalized(_seasonCycleTrackerService.Cycle, _seasonCycleTrackerService.Day);
        _tooltipText = _seasonService.CurrentSeason.SeasonType.Name;
        /*_tooltipText = _loc.T(_weatherService.IsHazardousWeather
            ? _hazardousWeatherUIHelper.NameLocKey
            : WeatherTemperateLocKey);*/
    }
}