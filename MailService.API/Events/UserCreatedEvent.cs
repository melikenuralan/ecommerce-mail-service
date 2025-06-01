namespace MailService.API.Events
{
    public class UserCreatedEvent
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
