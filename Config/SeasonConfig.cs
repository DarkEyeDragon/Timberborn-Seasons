using System.Collections.Generic;
using FloodSeason.Seasons.Types;
using TimberApi.ConfigSystem;

namespace FloodSeason.Config;

public class SeasonConfig
{
    private List<SeasonType> Seasons = new();

    public SeasonConfig()
    {
        Seasons.Add(new Spring());
        Seasons.Add(new Summer());
        Seasons.Add(new Autumn());
        Seasons.Add(new Winter());
    }
}