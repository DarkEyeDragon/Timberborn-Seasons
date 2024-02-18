using Bindito.Core;
using HarmonyLib;
using Seasons.UI;
using Timberborn.WeatherSystemUI;

namespace Seasons.Patches;

[HarmonyPatch(typeof(WeatherSystemUIConfigurator), nameof(WeatherSystemUIConfigurator.Configure))]
public class WeatherSystemUIConfiguratorPatch
{
    //TODO FIx
    static bool Prefix(IContainerDefinition containerDefinition)
    {
        SeasonsPlugin.ConsoleWriter.LogInfo($"Altering WeatherSystemUIConfigurator...");
        containerDefinition.Bind<SeasonDatePanel>().AsSingleton();
        //containerDefinition.Bind<DroughtNotifier>().AsSingleton();
        //containerDefinition.Bind<SeasonFastForwarder>().AsSingleton();
        //containerDefinition.Bind<SeasonWeatherPanel>().AsSingleton();
        
        //Load default weather panel for now
        containerDefinition.Bind<WeatherPanel>().AsSingleton();
        //containerDefinition.MultiBind<IConsoleModule>().To<SeasonFastForwarder>().AsSingleton();
        return false;
    }
}