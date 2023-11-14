using Seasons.WeatherLogic.Modifiers;

namespace Seasons.Calender;

public class Day
{
    //TODO make more generic
    public WaterSourceModifier Modifier { get; set; }
    public int Temperature { get; set; }
}