namespace Taav.Contracts.Notifications.PushNotifications.Dto;

public class SendMessagePushNotificationDto
{
    public SendMessagePushNotificationDto(
        int id, string body)
    {
        Id = id;
        Body = body;
    }

    [Required]
    public int Id { get; private set; }
    [Required]
    public string Body { get; set; }
    public string? ContextName { get; set; }
    public NotificationPayload? Payload { get; set; }
    public string? Title { get; set; }
    public DateTime? TimeToLive { get; set; }
    public string? ChannelKey { get; set; }
    public string? ChannelName { get; set; }
    public string? ChannelDescription { get; set; }
    public PushNotificationChannelImportance? Importance { get; set; }
    public PushNotificationLayout? Layout { get; set; }
    public string? LargeIcon { get; set; }
    public string? BigPicture { get; set; }
    public PushNotificationPriority? Priority { get; set; }
    public string? WebActionLink { get; set; }
}