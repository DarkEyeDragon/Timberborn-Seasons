using Bindito.Core;
using HarmonyLib;
using Timberborn.WeatherSystem;

namespace Seasons.Patches;

[HarmonyPatch(typeof(WeatherSystemConfigurator), nameof(WeatherSystemConfigurator.Configure))]

public class WeatherSystemConfiguratorPatch
{
    static void Prefix(IContainerDefinition containerDefinition)
    {
        SeasonsPlugin.ConsoleWriter.LogInfo($"Disabling default {nameof(WeatherSystemConfigurator)}...");
    }
}