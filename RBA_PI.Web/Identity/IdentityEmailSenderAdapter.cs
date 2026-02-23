using Microsoft.AspNetCore.Identity.UI.Services;

namespace RBA_PI.Web.Identity
{
    public class IdentityEmailSenderAdapter(Application.Interfaces.IEmailSender emailSender) : IEmailSender
    {
        private readonly Application.Interfaces.IEmailSender _emailSender = emailSender;

        public Task SendEmailAsync(string email, string subject, string htmlMessage) => _emailSender.SendAsync(email, subject, htmlMessage);
    }
}
