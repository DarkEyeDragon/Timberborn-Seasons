using Timberborn.Common;

namespace Seasons.Extensions;

public static class RandomNumberGeneratorExtension
{
    public static int Range(this IRandomNumberGenerator randomNumberGenerator, MinMax<int> minMax)
    {
        return randomNumberGenerator.Range(minMax.Min, minMax.Max);
    }
    
    public static float Random(this IRandomNumberGenerator randomNumberGenerator, MinMax<float> minMax)
    {
        return randomNumberGenerator.Range(minMax.Min, minMax.Max);
    }
}