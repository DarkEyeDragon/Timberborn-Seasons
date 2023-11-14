using System.Collections.Generic;
using Seasons.Seasons.Types;

namespace Seasons.Config;

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