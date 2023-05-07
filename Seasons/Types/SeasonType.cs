using FloodSeason.Seasons.Textures;
using FloodSeason.WeatherLogic.Modifiers;
using Timberborn.Common;

namespace FloodSeason.Seasons.Types;

public abstract class SeasonType : ISeasonType
{
    
    //public static readonly int DaysInSeason = 5;

    public abstract string Name { get; }
    public abstract MinMax<int> TemperatureRange { get; }

    public abstract IModifier[] Modifiers { get; }
    public override string ToString()
    {
        return $"Season: [{Name} {TemperatureRange.Min}-{TemperatureRange.Max}°C]";
    }

    public abstract int Order { get; }
    
    public virtual TexturePath TexturePath => new()
    {
        PathGrass = $"{TexturePathTypes.Terrain.Grass}{Name}",
        PathDesert = $"{TexturePathTypes.Terrain.Desert}{Name}"
    };

    public abstract bool IsDifficult { get; }
}