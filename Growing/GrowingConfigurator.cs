using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;
using Timberborn.TemplateSystem;

namespace FloodSeason.Growing;

[Configurator(SceneEntrypoint.InGame)]
public class GrowingConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.MultiBind<TemplateModule>().ToProvider<GrowingTemplateModuleProvider>().AsSingleton();
    }
}