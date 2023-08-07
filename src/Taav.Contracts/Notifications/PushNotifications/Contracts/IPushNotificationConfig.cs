namespace Taav.Contracts.Notifications.PushNotifications.Contracts;

public interface IPushNotificationConfig
{
    public string BaseUrl { get; }
    public string ContextName { get; }
}
