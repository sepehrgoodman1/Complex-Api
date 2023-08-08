using Taav.Contracts.Notifications.PushNotifications.Dto;

namespace Taav.Contracts.Notifications.PushNotifications.Builders;

public class SendMessageToConditionDtoBuilder
{
    private readonly SendMessagePushNotificationToConditionDto _dto;

    public SendMessageToConditionDtoBuilder(string body, string condition)
    {
        var messageId = Math.Abs(Guid.NewGuid().GetHashCode());
        _dto = new SendMessagePushNotificationToConditionDto(
            messageId, body, condition);
    }

    public SendMessageToConditionDtoBuilder WithPayloadAction(
        string payloadAction)
    {
        if (_dto.Payload == null)
            _dto.Payload = new NotificationPayload();

        _dto.Payload.Action = payloadAction;

        return this;
    }

    public SendMessageToConditionDtoBuilder WithPayloadArgument(
        string key, dynamic value)
    {
        if (_dto.Payload == null)
            _dto.Payload = new NotificationPayload();

        if (_dto.Payload.Arguments == null)
            _dto.Payload.Arguments = new Dictionary<string, dynamic>();

        _dto.Payload.Arguments.Add(key, value);

        return this;
    }

    public SendMessageToConditionDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;

        return this;
    }

    public SendMessageToConditionDtoBuilder WithTimeToLive(DateTime timeToLive)
    {
        _dto.TimeToLive = timeToLive;

        return this;
    }

    public SendMessageToConditionDtoBuilder WithChannel(
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

    public SendMessageToConditionDtoBuilder WithLayout(
        PushNotificationLayout layout)
    {
        _dto.Layout = layout;

        return this;
    }

    public SendMessageToConditionDtoBuilder WithLargeIcon(
        string largeIcon)
    {
        _dto.LargeIcon = largeIcon;

        return this;
    }

    public SendMessageToConditionDtoBuilder WithBigPicture(
        string bigPicture)
    {
        _dto.Layout = PushNotificationLayout.BigPicture;
        _dto.BigPicture = bigPicture;

        return this;
    }

    public SendMessageToConditionDtoBuilder WithPriority(
        PushNotificationPriority priority)
    {
        _dto.Priority = priority;

        return this;
    }

    public SendMessageToConditionDtoBuilder WithWebActionLink(
        string webActionLink)
    {
        _dto.WebActionLink = webActionLink;

        return this;
    }

    public SendMessagePushNotificationToConditionDto Build()
    {
        return _dto;
    }
}