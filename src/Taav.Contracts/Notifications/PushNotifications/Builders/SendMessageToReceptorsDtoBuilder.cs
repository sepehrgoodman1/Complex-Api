using Taav.Contracts.Notifications.PushNotifications.Dto;

namespace Taav.Contracts.Notifications.PushNotifications.Builders;

public class SendMessageToReceptorsDtoBuilder
{
    private readonly SendMessagePushNotificationsToReceptorsDto _dto;

    public SendMessageToReceptorsDtoBuilder(string body)
    {
        var messageId = Math.Abs(Guid.NewGuid().GetHashCode());
        _dto = new SendMessagePushNotificationsToReceptorsDto(
            messageId, body);
    }

    public SendMessageToReceptorsDtoBuilder WithReceptor(string receptor)
    {
        _dto.Receptors.Add(receptor);

        return this;
    }

    public SendMessageToReceptorsDtoBuilder WithPayloadAction(
        string payloadAction)
    {
        if (_dto.Payload == null)
            _dto.Payload = new NotificationPayload();
        
        _dto.Payload.Action = payloadAction;

        return this;
    }

    public SendMessageToReceptorsDtoBuilder WithPayloadArgument(
        string key, dynamic value)
    {
        if (_dto.Payload == null)
            _dto.Payload = new NotificationPayload();

        if (_dto.Payload.Arguments == null)
            _dto.Payload.Arguments = new Dictionary<string, dynamic>();
        
        _dto.Payload.Arguments.Add(key, value);

        return this;
    }

    public SendMessageToReceptorsDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;

        return this;
    }

    public SendMessageToReceptorsDtoBuilder WithTimeToLive(DateTime timeToLive)
    {
        _dto.TimeToLive = timeToLive;

        return this;
    }

    public SendMessageToReceptorsDtoBuilder WithChannel(
        string key,
        string name,
        string description,
        PushNotificationChannelImportance importance)
    {
        _dto.ChannelKey = key;
        _dto.ChannelName = name;
        _dto.ChannelDescription = description;
        _dto.Importance = importance;

        return this;
    }

    public SendMessageToReceptorsDtoBuilder WithLayout(
        PushNotificationLayout layout)
    {
        _dto.Layout = layout;

        return this;
    }

    public SendMessageToReceptorsDtoBuilder WithLargeIcon(
        string largeIcon)
    {
        _dto.LargeIcon = largeIcon;

        return this;
    }

    public SendMessageToReceptorsDtoBuilder WithBigPicture(
        string bigPicture)
    {
        _dto.Layout = PushNotificationLayout.BigPicture;
        _dto.BigPicture = bigPicture;

        return this;
    }

    public SendMessageToReceptorsDtoBuilder WithPriority(
        PushNotificationPriority priority)
    {
        _dto.Priority = priority;

        return this;
    }

    public SendMessageToReceptorsDtoBuilder WithWebActionLink(
        string webActionLink)
    {
        _dto.WebActionLink = webActionLink;

        return this;
    }

    public SendMessagePushNotificationsToReceptorsDto Build()
    {
        return _dto;
    }
}