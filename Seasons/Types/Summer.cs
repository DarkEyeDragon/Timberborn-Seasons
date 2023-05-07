using FloodSeason.Seasons.Textures;
using FloodSeason.WeatherLogic;
using FloodSeason.WeatherLogic.Modifiers;
using Timberborn.Common;

namespace FloodSeason.Seasons.Types;

public class Summer : SeasonType
{
    public override string Name => "Summer";
    public override MinMax<int> TemperatureRange => new(20, 40);
    private static float _baseMultiplier = 0.3f;

    public override IModifier[] Modifiers { get; } = new[]
    {
        new WaterSourceModifier(0.3f, _baseMultiplier, WeatherType.Sun),
        new WaterSourceModifier(0.7f, 0, WeatherType.Drought, true)
    };

    public override int Order => 1;

    public override TexturePath TexturePath => new()
    {
        PathDesert = null,
        PathGrass = base.TexturePath.PathGrass
    };
    
    public override bool IsDifficult => true;
}