using Bindito.Core;
using Timberborn.Growing;
using Timberborn.TemplateSystem;

namespace Seasons.Growing;

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