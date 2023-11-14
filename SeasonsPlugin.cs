using HarmonyLib;
using Seasons.Config;
using TimberApi.ConsoleSystem;
using TimberApi.ModSystem;

namespace Seasons
{
    //[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class SeasonsPlugin : IModEntrypoint
    {
        public static SeasonConfig Config;
        
        public const string PluginGuid = "me.darkeyedragon.seasons";
        public const string PluginName = "Seasons";
        
        public static IConsoleWriter ConsoleWriter;
        public void Entry(IMod mod, IConsoleWriter consoleWriter)
        {
            /*ConsoleWriter = consoleWriter;
            consoleWriter.LogInfo($"Loaded {PluginName} Code Entry Point...");*/
            new Harmony(PluginGuid).PatchAll();
        }
    }
}
