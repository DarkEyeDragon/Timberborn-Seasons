using System;
using HarmonyLib;
using Seasons.WeatherLogic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.NotificationSystem;
using UnityEngine;

namespace Seasons.Patches.Notifications;

[HarmonyPatch(typeof(NotificationBus), nameof(NotificationBus.Post))]
public class NotificationBusPatch
{
    static bool Prefix(string description, BaseComponent subject,
        ref EventHandler<NotificationEventArgs> ___NotificationPosted)
    {
        Guid entityId = subject.GetComponentFast<EntityComponent>().EntityId;
        var seasonWeatherService = TimberApi.DependencyContainerSystem.DependencyContainer
            .GetInstance<SeasonCycleTrackerService>();
        Notification notification = new Notification(description, entityId, seasonWeatherService.Cycle,
            seasonWeatherService.TotalCycles, Time.frameCount);
        EventHandler<NotificationEventArgs> notificationPosted = ___NotificationPosted;
        if (notificationPosted == null) return false;
        notificationPosted(seasonWeatherService, new NotificationEventArgs(notification));
        return false;
    }
}