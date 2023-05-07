using Bindito.Core;
using FloodSeason.Calender;
using FloodSeason.WeatherLogic.Modifiers;
using FloodSeason.WeatherLogic.Serializers;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace FloodSeason.WeatherLogic;

[Configurator(SceneEntrypoint.InGame)]
public class WeatherConfigurator : IConfigurator
{
    public void Configure(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<WaterSourceModifierSerializer>().AsSingleton();
        containerDefinition.Bind<DayObjectSerializer>().AsSingleton();
    }
}