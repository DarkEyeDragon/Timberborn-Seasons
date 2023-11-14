using Bindito.Core;
using Seasons.Calender;
using Seasons.WeatherLogic.Serializers;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace Seasons.WeatherLogic;

[Configurator(SceneEntrypoint.InGame)]
public class WeatherConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<WaterSourceModifierSerializer>().AsSingleton();
        containerDefinition.Bind<DayObjectSerializer>().AsSingleton();
    }
}