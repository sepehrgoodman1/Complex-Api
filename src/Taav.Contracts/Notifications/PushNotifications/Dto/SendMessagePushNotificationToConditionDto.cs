namespace Taav.Contracts.Notifications.PushNotifications.Dto
{
    public class SendMessagePushNotificationToConditionDto
        : SendMessagePushNotificationDto
    {
        public SendMessagePushNotificationToConditionDto(
            int id,
            string body,
            string condition)
            : base(id, body)
        {
            Condition = condition;
        }

        [Required]
        public string Condition { get; set; }
    }
}
