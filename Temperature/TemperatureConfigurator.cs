using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace Seasons.Temperature;
[Configurator(SceneEntrypoint.InGame)]
public class TemperatureConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<TemperatureService>().AsSingleton();
    }
}