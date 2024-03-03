using Bindito.Core;
using Seasons.SeasonSystem;
using Timberborn.HazardousWeatherSystemUI;
using Timberborn.Localization;

namespace Seasons.WeatherSystemUI;

public class SeasonWeatherUISpecification : IHazardousWeatherUISpecification
{
    private readonly SeasonService _seasonService;
    private readonly ILoc _loc;

    public SeasonWeatherUISpecification(SeasonService seasonService, ILoc loc)
    {
        _seasonService = seasonService;
        _loc = loc;
    }

    /*public string NameLocKey => $"{_loc.T("Seasons.Notifications.Title", _seasonService.CurrentSeason.SeasonType.Name)}";
    public string ApproachingLocKey => $"{_loc.T("Seasons.Notifications.Approaching", _seasonService.CurrentSeason.SeasonType.Name)}";
    public string InProgressLocKey => $"{_loc.T("Seasons.Notifications.InProgress", _seasonService.CurrentSeason.SeasonType.Name)}";
    public string StartedNotificationLocKey => $"{_loc.T("Seasons.Notifications.Started", _seasonService.CurrentSeason.SeasonType.Name)}";
    public string EndedNotificationLocKey => $"{_loc.T("Seasons.Notifications.Ended", _seasonService.CurrentSeason.SeasonType.Name)}";
    public string InProgressClass => "Weather.BadtideEndedNotification";
    public string IconClass => "date-panel--badtide";
    public string NotificationBackgroundClass => "hazardous-weather-notification__background--badtide";*/
    
    public string NameLocKey => "Seasons.Notifications.Title";
    public string ApproachingLocKey => "Seasons.Notifications.Approaching";
    public string InProgressLocKey => "Seasons.Notifications.InProgress";
    public string StartedNotificationLocKey => "Seasons.Notifications.Started";
    public string EndedNotificationLocKey => "Seasons.Notifications.Ended";
    public string InProgressClass => "Weather.BadtideEndedNotification";
    public string IconClass => "date-panel--badtide";
    public string NotificationBackgroundClass => "hazardous-weather-notification__background--badtide";
}