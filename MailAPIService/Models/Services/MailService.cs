using MailAPIService.Models.Configs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;


namespace MailAPIService.Models.Services
{
    public class MailService : Interfaces.IMailService
    {
        private MailboxAddress server;
        private MailServerInfo mailServerInfo;

        public MailService(IOptions<Config> config)
        {
            mailServerInfo = config.Value.MailServerInfo;
            server = new(mailServerInfo.DisplayName.Trim(), mailServerInfo.ServerAddress.Trim());
        }

        public async Task<SendingResult> TrySendMessage(SendingMessage message)
        {
            var mimemessage = new MimeMessage();
            mimemessage.From.Add(server);
            mimemessage.To.Add(new MailboxAddress(message.EMail, message.EMail));
            mimemessage.Subject = message.Subject;
            mimemessage.Body = new TextPart("plain")
            {
                Text = message.Body
            };
            using SmtpClient smtpClient = new();
            smtpClient.Connect(mailServerInfo.SMTPHost, Convert.ToInt32(mailServerInfo.SMTPPort), SecureSocketOptions.StartTls);
            smtpClient.Authenticate(mailServerInfo.ServerAddress, mailServerInfo.AuthPassword);
            SendingResult result;
            try
            {
                await smtpClient.SendAsync(mimemessage);
                result = new SendingResult { Result = Enums.Result.OK };
            }
            catch (Exception ex)
            {
                result = new SendingResult { Result = Enums.Result.Failed, ErrorMessage = ex.Message };
            }
            result.DateTime = DateTime.Now;
            smtpClient.Disconnect(true);
            return result;
        }
    }
}
