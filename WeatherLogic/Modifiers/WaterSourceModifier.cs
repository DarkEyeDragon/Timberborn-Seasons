namespace FloodSeason.WeatherLogic.Modifiers;

public class WaterSourceModifier : IModifier
{
    public float Weight { get; }
    public float Multiplier { get; }

    public bool IsNegative { get; }
    public WeatherType WeatherType { get; }

    public WaterSourceModifier(float weight, float multiplier, WeatherType weatherType, bool isNegative = false)
    {
        Weight = weight;
        Multiplier = multiplier;
        WeatherType = weatherType;
        IsNegative = isNegative;
    }
}