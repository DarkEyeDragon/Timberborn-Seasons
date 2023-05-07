using FloodSeason.Seasons.Textures;
using FloodSeason.WeatherLogic;
using FloodSeason.WeatherLogic.Modifiers;
using Timberborn.Common;

namespace FloodSeason.Seasons.Types;

public class Spring : SeasonType
{
    private static float _baseMultiplier = 1.5f;
    public override string Name => "Spring";
    public override MinMax<int> TemperatureRange => new(10, 20);

    public override IModifier[] Modifiers { get; } =
    {
        new WaterSourceModifier(0.3f, _baseMultiplier * 0.8f, WeatherType.Sun),
        new WaterSourceModifier(0.4f, _baseMultiplier * 1.3f, WeatherType.Rain),
        new WaterSourceModifier(0.2f, _baseMultiplier * 3f, WeatherType.Flood, true),
        new WaterSourceModifier(0.3f, _baseMultiplier, WeatherType.Wind)
    };

    public override int Order => 0;

    //Set to null to use default textures
    public override TexturePath TexturePath => null;
    public override bool IsDifficult => false;
}