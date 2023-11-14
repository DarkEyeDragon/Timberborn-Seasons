namespace Seasons.Patches.Notifications;

/*[HarmonyPatch(typeof(NotificationBus), nameof(NotificationBus.Post))]
public class NotificationBusPatch
{
    static bool Prefix(string description, GameObject subject, ref EventHandler<NotificationEventArgs> ___NotificationPosted)
    {
        Guid entityId = subject.GetComponent<EntityComponent>().EntityId;
        var seasonWeatherService = TimberApi.DependencyContainerSystem.DependencyContainer.GetInstance<SeasonCycleTrackerService>();
        Notification notification = new Notification(description, entityId, seasonWeatherService.Cycle, seasonWeatherService.Day);
        EventHandler<NotificationEventArgs> notificationPosted = ___NotificationPosted;
        if (notificationPosted == null) return false;
        notificationPosted(seasonWeatherService, new NotificationEventArgs(notification));
        return false;
    }
}*/