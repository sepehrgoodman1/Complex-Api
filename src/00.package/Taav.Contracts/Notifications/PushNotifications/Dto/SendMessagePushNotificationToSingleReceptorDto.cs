namespace Taav.Contracts.Notifications.PushNotifications.Dto
{
    public class SendMessagePushNotificationToSingleReceptorDto
        : SendMessagePushNotificationDto
    {
        public SendMessagePushNotificationToSingleReceptorDto(
            int id,
            string body,
            string receptor
        )
            : base(id, body)
        {
            Receptor = receptor;
        }

        [Required]
        public string Receptor { get; set; }
    }
}
