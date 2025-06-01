namespace MailService.API.Events
{
    public class PasswordResetEvent
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
