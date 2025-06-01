namespace MailService.API.Models
{
    public class MailRequestDto
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}
