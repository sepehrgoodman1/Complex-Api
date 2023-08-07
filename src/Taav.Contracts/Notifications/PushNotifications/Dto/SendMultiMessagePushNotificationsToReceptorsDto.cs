namespace Taav.Contracts.Notifications.PushNotifications.Dto;

public class SendMultiMessagePushNotificationsToReceptorsDto
{
    public SendMultiMessagePushNotificationsToReceptorsDto()
    {
        Notifications =
            new List<SendMessagePushNotificationsToReceptorsDto>();
    }

    public List<SendMessagePushNotificationsToReceptorsDto>
        Notifications
    { get; set; }
}
