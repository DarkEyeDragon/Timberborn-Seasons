using FloodSeason.WeatherLogic.Modifiers;
using FloodSeason.WeatherLogic.Serializers;
using Timberborn.Persistence;

namespace FloodSeason.Calender;

public class DayObjectSerializer : IObjectSerializer<Day>
{
    private static readonly PropertyKey<int> TemperatureKey = new("Temperature");
    private static readonly PropertyKey<WaterSourceModifier> WaterSourceModifierKey = new("WaterSourceModifier");
    private readonly WaterSourceModifierSerializer _waterSourceModifierSerializer;

    public DayObjectSerializer(WaterSourceModifierSerializer waterSourceModifierSerializer)
    {
        _waterSourceModifierSerializer = waterSourceModifierSerializer;
    }

    public void Serialize(Day value, IObjectSaver objectSaver)
    {
        objectSaver.Set(TemperatureKey, value.Temperature);
        objectSaver.Set(WaterSourceModifierKey, value.Modifier, _waterSourceModifierSerializer);
    }

    public Obsoletable<Day> Deserialize(IObjectLoader objectLoader)
    {
        var temp = objectLoader.Get(TemperatureKey);
        return !objectLoader.GetObsoletable(WaterSourceModifierKey, _waterSourceModifierSerializer, out var sourceModifier)
            ? new Obsoletable<Day>()
            : new Day { Modifier = sourceModifier, Temperature = temp };
    }
}