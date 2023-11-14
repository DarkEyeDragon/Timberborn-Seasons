using System.Collections.Generic;
using Seasons.Calender;
using Seasons.Seasons.Types;

namespace Seasons.Seasons;

public class Season
{
    public Season(int seasonIndex, Queue<Day> remainingDays, SeasonType seasonType)
    {
        SeasonIndex = seasonIndex;
        RemainingDays = remainingDays;
        SeasonType = seasonType;
        CurrentDay = RemainingDays.Peek();
        //TODO get from constructor
        PassedDays = new List<Day>();
        TotalDays = RemainingDays.Count;
    }

    public int TotalDays { get; }

    public int SeasonIndex { get; }
    public Queue<Day> RemainingDays { get; }
    /// <summary>
    /// The passed days since the start of the season.
    /// Added in sequential order (day 1 = index 0)
    /// </summary>
    public List<Day> PassedDays { get; }

    public Day CurrentDay { get; set; }
    public SeasonType SeasonType { get; }

    public void NextDay()
    {
        PassedDays.Add(CurrentDay);
        CurrentDay = RemainingDays.Dequeue();
    }
}