using Timberborn.Persistence;

namespace Seasons.Seasons;

public class SeasonObjectSerializer : IObjectSerializer<Season>
{
    public void Serialize(Season value, IObjectSaver objectSaver)
    {
        
    }

    public Obsoletable<Season> Deserialize(IObjectLoader objectLoader)
    {
        throw new System.NotImplementedException();
    }
}