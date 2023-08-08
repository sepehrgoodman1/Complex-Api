using Taav.Contracts.Notifications.PushNotifications.Dto;

namespace Taav.Contracts.Notifications.PushNotifications.Builders;

public class SendMessageToReceptorDtoBuilder
{
    private readonly SendMessagePushNotificationToSingleReceptorDto _dto;

    public SendMessageToReceptorDtoBuilder(string body, string receptor)
    {
        var messageId = Math.Abs(Guid.NewGuid().GetHashCode());
        _dto = new SendMessagePushNotificationToSingleReceptorDto(
            messageId, body, receptor);
    }

    public SendMessageToReceptorDtoBuilder WithPayloadAction(
        string payloadAction)
    {
        if (_dto.Payload == null)
            _dto.Payload = new NotificationPayload();
        
        _dto.Payload.Action = payloadAction;

        return this;
    }

    public SendMessageToReceptorDtoBuilder WithPayloadArgument(
        string key, dynamic value)
    {
        if (_dto.Payload == null)
            _dto.Payload = new NotificationPayload();

        if (_dto.Payload.Arguments == null)
            _dto.Payload.Arguments = new Dictionary<string, dynamic>();
        
        _dto.Payload.Arguments.Add(key, value);

        return this;
    }

    public SendMessageToReceptorDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;

        return this;
    }

    public SendMessageToReceptorDtoBuilder WithTimeToLive(DateTime timeToLive)
    {
        _dto.TimeToLive = timeToLive;

        return this;
    }

    public SendMessageToReceptorDtoBuilder WithChannel(
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

    public SendMessageToReceptorDtoBuilder WithLayout(
        PushNotificationLayout layout)
    {
        _dto.Layout = layout;

        return this;
    }

    public SendMessageToReceptorDtoBuilder WithLargeIcon(
        string largeIcon)
    {
        _dto.LargeIcon = largeIcon;

        return this;
    }

    public SendMessageToReceptorDtoBuilder WithBigPicture(
        string bigPicture)
    {
        _dto.Layout = PushNotificationLayout.BigPicture;
        _dto.BigPicture = bigPicture;

        return this;
    }

    public SendMessageToReceptorDtoBuilder WithPriority(
        PushNotificationPriority priority)
    {
        _dto.Priority = priority;

        return this;
    }

    public SendMessageToReceptorDtoBuilder WithWebActionLink(
        string webActionLink)
    {
        _dto.WebActionLink = webActionLink;

        return this;
    }

    public SendMessagePushNotificationToSingleReceptorDto Build()
    {
        return _dto;
    }
}