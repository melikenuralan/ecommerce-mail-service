using MailService.API.Models;
using MailService.API.Services;
using MailService.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailService.API.Controllers
{
    [ApiController]
    [Route("api/mail")]
    public class MailController : ControllerBase
    {
        private readonly ISmtpMailService _mailService;

        public MailController(ISmtpMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromBody] MailRequestDto dto)
        {
            await _mailService.SendMailAsync(dto);
            return Ok("Mail gönderildi.");
        }
    }
}
