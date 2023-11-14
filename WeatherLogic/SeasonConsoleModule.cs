using Seasons.Seasons;
using Timberborn.Debugging;

namespace Seasons.WeatherLogic;

public class SeasonConsoleModule : IConsoleModule
{
    private readonly SeasonService _seasonService;

    public SeasonConsoleModule(SeasonService seasonService)
    {
        _seasonService = seasonService;
    }

    public ConsoleModuleDefinition GetDefinition() => new ConsoleModuleDefinition.Builder().AddMethod(
        ConsoleMethod.Create(
            "Cycle Season",
            () => { _seasonService.NextSeason(); })
    ).Build();
}