using Taav.Contracts.Notifications.PushNotifications.Dto;

namespace Taav.Contracts.Notifications.PushNotifications.Builders;

public class SendMultiNotificationsToReceptorsDtoBuilder
{
    private readonly SendMultiMessagePushNotificationsToReceptorsDto _dto;

    public SendMultiNotificationsToReceptorsDtoBuilder()
    {
        _dto = new SendMultiMessagePushNotificationsToReceptorsDto();
    }

    public SendMultiNotificationsToReceptorsDtoBuilder WithNotification(
        SendMessagePushNotificationsToReceptorsDto notification)
    {
        _dto.Notifications.Add(notification);

        return this;
    }

    public SendMultiMessagePushNotificationsToReceptorsDto Build()
    {
        return _dto;
    }
}
