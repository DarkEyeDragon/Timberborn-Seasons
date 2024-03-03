using Bindito.Core;
using Seasons.ParticleSystem.Rain;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace Seasons.ParticleSystem;

[Configurator(SceneEntrypoint.InGame)]
public class RainParticleSystemConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<RainParticleSystem>().AsSingleton();
    }
}