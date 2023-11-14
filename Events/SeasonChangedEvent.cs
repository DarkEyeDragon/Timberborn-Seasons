using Seasons.Seasons;

namespace Seasons.Events;

public class SeasonChangedEvent
{
    public Season Season { get; }

    public SeasonChangedEvent(Season season)
    {
        Season = season;
    }
}