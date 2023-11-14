using UnityEngine;

namespace Seasons.Growing;

public class PlantableService
{
    //TODO deal with trees and berries
    public void PauseGrowth()
    {
        /*var crops = Object.FindObjectsOfType<Crop>();
        var farmhouses = Object.FindObjectsOfType<FarmHouse>();
        foreach (var farmHouse in farmhouses)
        {
            var pausableBuilding = farmHouse.GetComponentInParent<PausableBuilding>();
            pausableBuilding.Pause();
        }

        SeasonsPlugin.ConsoleWriter.LogInfo($"Amount: {crops.Length}");
        foreach (var crop in crops)
        {
            SeasonsPlugin.ConsoleWriter.LogInfo($"{crop.name}");
            var dryObject = crop.GetComponentInParent<DryObject>();
            //var living = crop.GetComponentInParent<LivingNaturalResource>();
            if (dryObject is not null)
            {
                dryObject.EnterDryState();
            }
            else
            {
                SeasonsPlugin.ConsoleWriter.LogInfo($"{crop.name} does not have {nameof(Crop)}");
            }
        }*/
        SeasonsPlugin.ConsoleWriter.LogInfo($"Pause Growth");
        var hibernateObjects = Object.FindObjectsOfType<HibernateObject>();
        foreach (var hibernateObject in hibernateObjects)
        {
            //SeasonsPlugin.ConsoleWriter.LogInfo($"{hibernateObject.name}");
            //var living = crop.GetComponentInParent<LivingNaturalResource>();
            hibernateObject.EnterHibernateState();
        }
    }

    public void ResumeGrowth()
    {
        var hibernateObjects = Object.FindObjectsOfType<HibernateObject>();
        /*var farmhouses = Object.FindObjectsOfType<FarmHouse>();
        foreach (var farmHouse in farmhouses)
        {
            var pausableBuilding = farmHouse.GetComponentInParent<PausableBuilding>();
            pausableBuilding.Resume();
        }*/
        SeasonsPlugin.ConsoleWriter.LogInfo($"Resume Growth");
        foreach (var hibernateObject in hibernateObjects)
        {
            //SeasonsPlugin.ConsoleWriter.LogInfo($"{seasonal.name}");
            //var living = crop.GetComponentInParent<LivingNaturalResource>();
            hibernateObject.ExitHibernateState();
        }
    }
}