using Taav.Contracts.Notifications.PushNotifications.Dto;

namespace Taav.Contracts.Notifications.PushNotifications.Contracts;

public interface IPushNotificationService
{
    Task SendMessageToReceptor(
        SendMessagePushNotificationToSingleReceptorDto dto);

    Task SendMessageToReceptors(
        SendMessagePushNotificationsToReceptorsDto dto);

    Task SendByCondition(
        SendMessagePushNotificationToConditionDto dto);

    Task SendMultiNotificationsToReceptors(
        SendMultiMessagePushNotificationsToReceptorsDto dto);
}
