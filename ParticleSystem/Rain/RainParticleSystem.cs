using Bindito.Core;
using Seasons.Patches.WindSystem;
using Seasons.SeasonSystem.Textures;
using Timberborn.AssetSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Core;
using Timberborn.SingletonSystem;
using Timberborn.WindSystem;
using UnityEngine;

namespace Seasons.ParticleSystem.Rain;

public class RainParticleSystem : ILoadableSingleton
{
    private IResourceAssetLoader _resourceAssetLoader;
    private MapSize _mapSize;
    private UnityEngine.ParticleSystem _rain;
    private EventBus _eventbus;

    [Inject]
    public void Inject(IResourceAssetLoader resourceAssetLoader, MapSize mapSize,
        EventBus eventbus)
    {
        _resourceAssetLoader = resourceAssetLoader;
        _mapSize = mapSize;
        _eventbus = eventbus;
    }

    [OnEvent]
    public void OnWindChanged(SeasonWindChangedEvent windChangedEvent)
    {
        // var force = _rain.forceOverLifetime;
        // force.x = _windservice.WindDirection.x * _windservice.WindStrength;
        // force.z = _windservice.WindDirection.y * _windservice.WindStrength;
        var force = _rain.forceOverLifetime;
        force.x = windChangedEvent.WindDirection.x;
        force.z =  -windChangedEvent.WindDirection.y;
        force.xMultiplier = windChangedEvent.WindStrength;
        force.zMultiplier = windChangedEvent.WindStrength;
        SeasonsPlugin.ConsoleWriter.LogInfo($"Force: x {force.x.constant} - z {force.z.constant}");
        SeasonsPlugin.ConsoleWriter.LogInfo($"Force Multiplier: x {force.xMultiplier} - z {force.zMultiplier}");
    }

    public void Load()
    {
        _eventbus.Register(this);
        //var prefab = PrefabBuilder.Create<UnityEngine.ParticleSystem>("rain").Build();
        var particles = _resourceAssetLoader.Load<GameObject>($"{TexturePathTypes.Season}/Particles");
        //if(prefab.transform.position is null) SeasonsPlugin.ConsoleWriter.LogWarning("Prefab null");
        //prefab.GameObjectFast.transform.position = Vector3.zero;
        //SeasonsPlugin.ConsoleWriter.LogInfo(particles.name);

        var particle = Object.Instantiate(particles.FindChild("Snow"));
        particle.position = new Vector3(_mapSize.TerrainSize.x / 2, MapSize.MaxGameTerrainHeight + 10,
            _mapSize.TerrainSize.y / 2);
        SeasonsPlugin.ConsoleWriter.LogInfo($"Position: {particle.position.ToString()}");

        _rain = particle.GetComponent<UnityEngine.ParticleSystem>();
        var force = _rain.forceOverLifetime;
        force.enabled = true;

        var emission = _rain.emission;
        var rateOverTime = emission.rateOverTime = _mapSize.TerrainSize.x * _mapSize.TerrainSize.y /20;
        var shape = _rain.shape;
        var scale = shape.scale = new Vector3(_mapSize.TerrainSize.x, 1, _mapSize.TerrainSize.y);


        SeasonsPlugin.ConsoleWriter.LogInfo($"Scale: {scale.ToString()}");
        SeasonsPlugin.ConsoleWriter.LogInfo($"Amount of particles: {rateOverTime.constant}");
        //shape.scale = scale;
        _rain.Play();
    }
}