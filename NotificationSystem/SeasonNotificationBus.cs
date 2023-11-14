using System;
using Seasons.WeatherLogic;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Seasons.NotificationSystem;

public class SeasonNotificationBus
{
    private readonly SeasonCycleTrackerService _seasonCycleTrackerService;

    public event EventHandler<CustomNotificationEventArgs> NotificationPosted;
    
    public SeasonNotificationBus(SeasonCycleTrackerService seasonCycleTrackerService)
    {
        _seasonCycleTrackerService = seasonCycleTrackerService;
    }
    
    public void Post(string description, GameObject subject)
    {
        Guid entityId = subject.GetComponent<EntityComponent>().EntityId;
        var notification = new CustomNotification(description, entityId, _seasonCycleTrackerService.Cycle, _seasonCycleTrackerService.Day);
        EventHandler<CustomNotificationEventArgs> notificationPosted = NotificationPosted;
        if (notificationPosted == null)
            return;
        notificationPosted(this, new CustomNotificationEventArgs(notification));
    }
}