using Bindito.Core;
using Seasons.WaterLogic;
using Timberborn.TemplateSystem;
using Timberborn.WaterSourceSystem;

namespace Seasons.Seasons;

public class SeasonTemplateModuleProvider : IProvider<TemplateModule>
{
    public TemplateModule Get()
    {
        TemplateModule.Builder builder = new TemplateModule.Builder();
        builder.AddDecorator<WaterSource, SeasonWaterSourceController>();
        return builder.Build();
    }
}