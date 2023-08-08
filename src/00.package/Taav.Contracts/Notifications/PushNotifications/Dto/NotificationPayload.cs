namespace Taav.Contracts.Notifications.PushNotifications.Dto;

public class NotificationPayload
{
    public string Action { get; set; }
    public Dictionary<string, dynamic>? Arguments { get; set; }
}
