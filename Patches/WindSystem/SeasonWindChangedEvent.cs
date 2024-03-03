using UnityEngine;

namespace Seasons.Patches.WindSystem;

public class SeasonWindChangedEvent
{
    public Vector2 WindDirection { get; }
    public float WindStrength { get; }

    public SeasonWindChangedEvent(Vector2 windDirection, float windStrength)
    {
        WindDirection = windDirection;
        WindStrength = windStrength;
    }
}