using FloodSeason.WeatherLogic.Modifiers;
using Timberborn.Persistence;

namespace FloodSeason.WeatherLogic.Serializers;

public class WaterSourceModifierSerializer : IObjectSerializer<WaterSourceModifier>
{
    private static readonly PropertyKey<float> MultiplierKey = new("Multiplier");
    private static readonly PropertyKey<float> WeightKey = new("Weight");
    private static readonly PropertyKey<bool> NegativeKey = new("Negative");
    private static readonly PropertyKey<int> WeatherTypeKey = new("WeatherType");

    public void Serialize(WaterSourceModifier value, IObjectSaver objectSaver)
    {
        objectSaver.Set(MultiplierKey, value.Multiplier);
        objectSaver.Set(WeightKey, value.Weight);
        objectSaver.Set(NegativeKey, value.IsNegative);
        objectSaver.Set(WeatherTypeKey, (int)value.WeatherType);
    }

    public Obsoletable<WaterSourceModifier> Deserialize(IObjectLoader objectLoader)
    {
        var multiplier = objectLoader.Get(MultiplierKey);
        var weight = objectLoader.Get(WeightKey);
        var negative = objectLoader.Get(NegativeKey);
        var weatherType = objectLoader.Get(WeatherTypeKey);
        return new Obsoletable<WaterSourceModifier>(new WaterSourceModifier(weight, multiplier,
            (WeatherType)weatherType, negative));
    }
}