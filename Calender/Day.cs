
using FloodSeason.WeatherLogic;
using FloodSeason.WeatherLogic.Modifiers;

namespace FloodSeason.Calender;

public class Day
{
    //TODO make more generic
    public WaterSourceModifier Modifier { get; set; }
    public int Temperature { get; set; }
}