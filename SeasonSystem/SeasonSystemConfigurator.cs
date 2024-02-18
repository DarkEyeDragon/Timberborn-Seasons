using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace Seasons.SeasonSystem;

[Configurator(SceneEntrypoint.InGame)]
public class SeasonSystemConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<SeasonService>().AsSingleton();
    }
}