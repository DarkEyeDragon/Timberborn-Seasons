using System;
using Timberborn.EntitySystem;
using Timberborn.Growing;
using Timberborn.Persistence;
using UnityEngine;

namespace FloodSeason.Growing;

public class HibernateObject : MonoBehaviour
{
    private Growable _growable;

    public bool IsHibernating { get; set; }
    
    public event EventHandler EnteredHibernateState;
    public event EventHandler ExitedHibernateState;

    public void Awake()
    {
        _growable = GetComponent<Growable>();
    }

    public void EnterHibernateState()
    {
        if(IsHibernating) return;
        IsHibernating = true;
        var hibernateState = EnteredHibernateState;
        if (hibernateState is null) return;
        hibernateState(this, EventArgs.Empty);
    }
    
    public void ExitHibernateState()
    {
        if(!IsHibernating) return;
        IsHibernating = false;
        var hibernateState = ExitedHibernateState;
        if (hibernateState is null) return;
        hibernateState(this, EventArgs.Empty);
    }
}