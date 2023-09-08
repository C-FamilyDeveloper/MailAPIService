using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using MailAPIService.Models.Abstractions;
using MailAPIService.Models.Configs;
using Microsoft.Extensions.Options;

namespace MailAPIService.Models.Services
{
    public  class MailService : IMailService
    {
        private readonly IOptions<Config> options;
        private MailAddress server;
        private SmtpClient smtpClient;

        public MailService(IOptions<Config> options) 
        {
            this.options = options;
            var serverAuth = options.Value.MailServerInfo;
            server = new(serverAuth.ServerAddress.Trim(), serverAuth.DisplayName.Trim());
            smtpClient = new(serverAuth.SMTPHost.Trim(), Convert.ToInt32(serverAuth.SMTPPort))
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(serverAuth.ServerAddress.Trim(),
                serverAuth.AuthPassword.Trim()),
                Timeout = 5000,
                EnableSsl = true
            };
        }

        public async Task SendMessageAsync([EmailAddress] string email, string subject, string body)
        {
            try
            {
                MailAddress address = new(email);
                MailMessage message = new(server, address)
                {
                    Subject = subject.Trim(),
                    Body = body.Trim(),
                };
                await smtpClient.SendMailAsync(message);
            }
            catch (Exception)
            {
                throw;
            }            
        }

    }
}
