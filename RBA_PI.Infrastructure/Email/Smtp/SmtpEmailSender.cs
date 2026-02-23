using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RBA_PI.Application.Interfaces;

namespace RBA_PI.Infrastructure.Email.Smtp
{
    public class SmtpEmailSender(IConfiguration config) : IEmailSender
    {
        private readonly IConfiguration _config = config;

        public async Task SendAsync(string email, string subject, string body)
        {
            MimeMessage message = new();

            message.From.Add(MailboxAddress.Parse(_config["Email:From"]));
            message.To.Add(MailboxAddress.Parse(email));

            message.Subject = subject;

            message.Body = new BodyBuilder
            {
                HtmlBody = body
            }.ToMessageBody();

            using SmtpClient client = new();

            string? host = _config["Email:Smtp"];
            int port = int.Parse(_config["Email:Port"]!);
            string? user = _config["Email:User"];
            string? pass = _config["Email:Password"];

            await client.ConnectAsync(host, port, SecureSocketOptions.None);

            if (!string.IsNullOrEmpty(user))
                await client.AuthenticateAsync(user, pass);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
