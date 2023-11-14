namespace Seasons.Seasons.Textures;

public class TexturePath
{
    public string PathDesert { get; set; }
    public string PathGrass { get; set; }
}

public static class TexturePathTypes
{
    private static readonly string Base = $"{PluginInfo.PLUGIN_GUID}";
    private static readonly string Season = $"{Base}/seasons";
    public static class Terrain
    {
        public static readonly string Desert = $"{Season}/Desert-";
        public static readonly string Grass = $"{Season}/Grass-";
    }
}
