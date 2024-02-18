using Seasons.SeasonSystem;
using Seasons.SeasonSystem.Textures;
using Seasons.SeasonSystem.Types;
using Seasons.WeatherLogic;
using Seasons.WeatherLogic.Modifiers;
using Seasons.WeatherSystem;
using Seasons.WeatherSystemUI;
using Timberborn.Common;
using Timberborn.HazardousWeatherSystemUI;

namespace Seasons.Seasons.Types;

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