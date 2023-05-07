using System;
using Timberborn.EntitySystem;
using Timberborn.Growing;
using Timberborn.NaturalResourcesModelSystem;
using Timberborn.Persistence;
using Timberborn.SoilMoistureSystem;
using UnityEngine;

namespace FloodSeason.Growing;

public class Seasonal : MonoBehaviour
{
    public void Awake()
    {
        var hibernateObject = GetComponent<HibernateObject>();
        var growable = GetComponent<Growable>();
        var dryObject = GetComponent<DryObject>();
        hibernateObject.EnteredHibernateState += (_, _) =>
        {
            growable.PauseGrowing();
            dryObject.EnterDryState();
        };
        hibernateObject.ExitedHibernateState += (_, _) =>
        {
            growable.ResumeGrowing();
            dryObject.EnterDryState();
        };
    }
}