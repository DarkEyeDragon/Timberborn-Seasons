using System.IO;
using Newtonsoft.Json;

namespace FloodSeason.Config;

public class ConfigHandler
{
    public SeasonConfig Config { get; set; }

    public void Load(string path)
    {
        if (!File.Exists(path))
        {
            Config = CreateDefaultConfig(path);
            return;
        }

        var configStr = File.ReadAllText(path);
        Config = JsonConvert.DeserializeObject<SeasonConfig>(configStr);
    }


    private static SeasonConfig CreateDefaultConfig(string path)
    {
        SeasonsPlugin.ConsoleWriter.LogInfo("Creating default config...");
        var config = new SeasonConfig();
        File.WriteAllText(path, JsonConvert.SerializeObject(config));
        return config;
    }
}