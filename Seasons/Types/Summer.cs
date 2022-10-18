using FloodSeason.Weather;
using FloodSeason.Weather.Modifiers;

namespace FloodSeason.Seasons.Types;

public class Summer : Season
{
    public override string Name => "Summer";
    public override int Temperature => 25;
    private static float _baseMultiplier = 0.3f;

    public override IModifier[] Modifiers { get; } = new[]
    {
        new WaterSourceModifier(0.3f, _baseMultiplier, WeatherType.Sun),
        new WaterSourceModifier(0.7f, 0, WeatherType.Drought, true)
    };
}