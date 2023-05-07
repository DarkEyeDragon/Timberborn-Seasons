﻿using Bindito.Core;
using FloodSeason.Growing;
using FloodSeason.Seasons;
using FloodSeason.WeatherLogic;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;
using Timberborn.Debugging;

namespace FloodSeason.WeatherSystem;

[Configurator(SceneEntrypoint.InGame)]
public class SeasonWeatherSystemConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<SeasonCycleTrackerService>().AsSingleton();
        containerDefinition.Bind<PlantableService>().AsSingleton();
        containerDefinition.Bind<SeasonService>().AsSingleton();
        containerDefinition.Bind<SeasonWeatherService>().AsSingleton();
        containerDefinition.MultiBind<IConsoleModule>().To<SeasonConsoleModule>().AsSingleton();
    }
}