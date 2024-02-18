using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;
using Timberborn.TemplateSystem;

namespace Seasons.SeasonSystem;

[Configurator(SceneEntrypoint.InGame)]
public class SeasonConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        //containerDefinition.Bind<SeasonNotificationBus>().AsSingleton();
        containerDefinition.MultiBind<TemplateModule>().ToProvider<SeasonTemplateModuleProvider>().AsSingleton();
    }
}