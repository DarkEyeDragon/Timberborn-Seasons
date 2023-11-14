using Seasons.Seasons.Textures;
using Seasons.WeatherLogic;
using Seasons.WeatherLogic.Modifiers;
using Timberborn.Common;

namespace Seasons.Seasons.Types;

public class Autumn : SeasonType
{
    private static float _baseMultiplier = 1.2f;
    public override string Name => "Autumn";
    public override MinMax<int> TemperatureRange => new(5, 15);

    public override IModifier[] Modifiers { get; } =
    {
        new WaterSourceModifier(0.2f, _baseMultiplier, WeatherType.Sun),
        new WaterSourceModifier(0.4f, _baseMultiplier * 1.3f, WeatherType.Rain),
        new WaterSourceModifier(0.2f, _baseMultiplier * 3f, WeatherType.Flood),
        new WaterSourceModifier(0.3f, _baseMultiplier, WeatherType.Wind)
    };

    public override int Order => 2;

    public override TexturePath TexturePath => new()
    {
        PathDesert = null,
        PathGrass = base.TexturePath.PathGrass
    };

    public override bool IsDifficult => false;
}