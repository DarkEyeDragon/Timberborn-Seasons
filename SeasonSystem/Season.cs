using System.Collections.Generic;
using Seasons.Calender;
using Seasons.Seasons.Types;
using Seasons.SeasonSystem.Types;

namespace Seasons.SeasonSystem;

public class Season
{
    public Season(SeasonType seasonType)
    {
        SeasonType = seasonType;
        //SeasonIndex = seasonIndex;
        //RemainingDays = remainingDays;
        //SeasonType = seasonType;
        //Weather = weather;
        //TODO get from constructor
        PassedDays = new List<Day>();
        //TotalDays = RemainingDays.Count;
    }

    public int TotalDays { get; private set; }

    //public int SeasonIndex { get; }
    public Queue<Day> 
        RemainingDays { get; private set; }
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

    public void SetForecast(Queue<Day> forecastDays)
    {
        RemainingDays = forecastDays;
        TotalDays = RemainingDays.Count;
        CurrentDay = forecastDays.Dequeue();
    }
}