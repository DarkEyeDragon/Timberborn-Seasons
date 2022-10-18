namespace FloodSeason.Utils;

public class MonthParser
{
    private static readonly string prefix = "seasons.month.";
    public static string ToTranslationKey(int month)
    {
        return $"{prefix}{month}";
    }
}