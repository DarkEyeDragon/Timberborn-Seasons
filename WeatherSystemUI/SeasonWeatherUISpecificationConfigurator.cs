using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace Seasons.WeatherSystemUI;

[Configurator(SceneEntrypoint.InGame)]
public class SeasonWeatherUISpecificationConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<SeasonWeatherUISpecification>().AsSingleton();
    }
}