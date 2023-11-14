using Bindito.Core;
using Timberborn.CameraSystem;
using Timberborn.SkySystem;

namespace Seasons.SkySystem;

public class SeasonSun : Sun
{
    [Inject]
    public void InjectDependencies(CameraComponent cameraComponent, SeasonDayStageCycle dayStageCycle)
    {
        _cameraComponent = cameraComponent;
        _dayStageCycle = dayStageCycle;
    }
}