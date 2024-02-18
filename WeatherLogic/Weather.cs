using Seasons.WeatherLogic.Modifiers;
using Timberborn.HazardousWeatherSystemUI;

namespace Seasons.WeatherLogic;

public class Weather
{
    public WeatherType WeatherType { get; }

    public IHazardousWeatherUISpecification WeatherUISpecification { get; }
    public IModifier Modifier { get; }


    public Weather(WeatherType weatherType, IModifier modifier, IHazardousWeatherUISpecification weatherUISpecification)
    {
        WeatherType = weatherType;
        Modifier = modifier;
        WeatherUISpecification = weatherUISpecification;
    }

    public override string ToString()
    {
        return $"{WeatherType}";
    }
}