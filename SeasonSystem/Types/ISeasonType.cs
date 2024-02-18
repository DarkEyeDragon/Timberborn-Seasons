using Seasons.SeasonSystem.Textures;

namespace Seasons.Seasons.Types;

public interface ISeasonType
{
    public int Order { get; }

    public TexturePath TexturePath { get; }

    public bool IsDifficult { get; }
}