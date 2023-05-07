using Bindito.Core;
using Timberborn.Growing;
using Timberborn.NaturalResourcesLifeCycle;
using Timberborn.TemplateSystem;

namespace FloodSeason.Growing;

public class GrowingTemplateModuleProvider : IProvider<TemplateModule>
{
    public TemplateModule Get()
    {
        TemplateModule.Builder builder = new TemplateModule.Builder();
        builder.AddDecorator<Growable, Seasonal>();
        builder.AddDecorator<Seasonal, HibernateObject>();
        return builder.Build();
    }
}