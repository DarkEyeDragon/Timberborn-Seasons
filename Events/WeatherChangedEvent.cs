using FloodSeason.Seasons;

namespace FloodSeason.Events;

public class WeatherChangedEvent
{
    public WeatherLogic.Weather Weather { get; }

    public WeatherChangedEvent(WeatherLogic.Weather weather)
    {
        Weather = weather;
    }
}