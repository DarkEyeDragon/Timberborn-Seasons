using System;
using Timberborn.Growing;
using Timberborn.NaturalResourcesModelSystem;
using Timberborn.PrefabSystem;
using Timberborn.SoilMoistureSystem;
using UnityEngine;

namespace Seasons.Growing;

public class Seasonal : MonoBehaviour
{
    public void Awake()
    {
        var hibernateObject = GetComponent<HibernateObject>();
        var growable = GetComponent<Growable>();
        var dryObject = GetComponent<DryObject>();
        var prefab = GetComponent<Prefab>();
        var naturalResourceLifecycleModel = GetComponent<NaturalResourceModel>();
        //Pine trees dont hibernate. So we dont need to listen to the event.
        if(prefab.PrefabName.Equals("Pine", StringComparison.CurrentCultureIgnoreCase))
            return;
        hibernateObject.EnteredHibernateState += (_, _) =>
        {
            //growable.PauseGrowing();
            dryObject.EnterDryState();
            naturalResourceLifecycleModel.ShowCurrentModel();
            
        };
        hibernateObject.ExitedHibernateState += (_, _) =>
        {
            //growable.ResumeGrowing();
            dryObject.ExitDryState();
            naturalResourceLifecycleModel.ShowCurrentModel();
        };
    }
}