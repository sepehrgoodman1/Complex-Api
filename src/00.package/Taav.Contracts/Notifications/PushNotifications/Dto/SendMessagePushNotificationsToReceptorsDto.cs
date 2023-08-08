namespace Taav.Contracts.Notifications.PushNotifications.Dto
{
    public class SendMessagePushNotificationsToReceptorsDto
        : SendMessagePushNotificationDto
    {
        public SendMessagePushNotificationsToReceptorsDto(
            int id,
            string body)
            : base(id, body)
        {
            Receptors = new List<string>();
        }

        [Required]
        public List<string> Receptors { get; set; }
    }
}
