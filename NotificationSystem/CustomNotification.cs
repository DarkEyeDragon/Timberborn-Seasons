using System;

namespace FloodSeason.NotificationSystem;

public class CustomNotification
{
    public string Description { get; }
    public int Cycle { get; }
    public int Day { get; }

    public CustomNotification(string description, Guid subject, int cycle, int day)
    {
        Description = description;
        Cycle = cycle;
        Day = day;
    }
}