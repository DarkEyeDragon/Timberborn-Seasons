namespace FloodSeason.WeatherLogic.Modifiers;

public interface IModifier
{
    public float Weight { get; }
    public float Multiplier { get; }
    
    public bool IsNegative { get; }
}