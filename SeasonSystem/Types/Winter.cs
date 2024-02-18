using Seasons.SeasonSystem.Types;
using Seasons.WeatherLogic;
using Seasons.WeatherLogic.Modifiers;
using Timberborn.Common;

namespace Seasons.Seasons.Types;

public class Winter : SeasonType
{
    private static float _baseMultiplier = 0f;

    public override string Name => "Winter";
    public override MinMax<int> TemperatureRange => new(-5, -30);

    public override IModifier[] Modifiers { get; } =
    {
        new WaterSourceModifier(0.3f, _baseMultiplier, WeatherType.Sun),
        new WaterSourceModifier(0.4f, _baseMultiplier, WeatherType.Rain),
        //TODO wind is not a watersource modifier
        new WaterSourceModifier(0.3f, _baseMultiplier, WeatherType.Wind)
    };

    public override int Order => 3;

    public override bool IsDifficult => true;
}