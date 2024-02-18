using Timberborn.CameraSystem;
using Timberborn.CoreUI;
using Timberborn.HazardousWeatherSystemUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using Timberborn.WeatherSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Seasons.WeatherSystem;

public class SeasonWeatherNotificationPanel : HazardousWeatherNotificationPanel
{
    public SeasonWeatherNotificationPanel(ILoc loc, EventBus eventBus, HazardousWeatherUIHelper hazardousWeatherUIHelper, UILayout uiLayout, PanelStack panelStack, VisualElementLoader visualElementLoader, WeatherService weatherService, CameraHorizontalShifter cameraHorizontalShifter) : base(loc, eventBus, hazardousWeatherUIHelper, uiLayout, panelStack, visualElementLoader, weatherService, cameraHorizontalShifter)
    {
        
    }
    
    
}