namespace Seasons.WeatherLogic.Modifiers;

public interface IModifier
{
    
    /// <summary>
    /// The chance to occur (0-1)
    /// </summary>
    public float Weight { get; }
    
    /// <summary>
    /// Severity Multiplier
    /// </summary>
    public float Multiplier { get; }
    
    /// <summary>
    /// If this is a negative modifier
    /// </summary>
    public bool IsNegative { get; }
}