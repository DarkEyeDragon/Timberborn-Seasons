using Seasons.Seasons;
using Seasons.SeasonSystem;
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
            "Seasons: Next Season",
            () =>
            {
                if (_seasonService.CurrentSeason.SeasonType.Order != _seasonService.SeasonTypes.Count - 1)
                {
                    _seasonService.NextSeason();
                }
                else
                {
                    _seasonService.NewYear();
                }
            })
    ).Build();
}