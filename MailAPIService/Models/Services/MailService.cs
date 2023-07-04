using System.Net;
using System.Net.Mail;
using MailAPIService.Models.Requests;

namespace MailerAPIService.Models.Services
{
    public  class MailService
    {
        private MailAddress server;
        private SmtpClient smtpClient;
        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        /// <param name="serverAuth">Класс с авторизационной информацией сервера</param>
        public MailService(MailServerInfo serverAuth) 
        {
            server = new(serverAuth.ServerAddress.Trim(), serverAuth.DisplayName.Trim());
            smtpClient = new(serverAuth.SMTPHost.Trim(), serverAuth.SMTPPort)
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(serverAuth.ServerAddress.Trim(),
                serverAuth.AuthPassword.Trim()),
                Timeout = 5000,
                EnableSsl = true
            };
        }
        /// <summary>
        /// Отправка сообщения по электронной почте, по заданным адресам
        /// </summary>
        /// <param name="mailMessageInfo">Класс с информацией об отправляемом сообщении и получателях</param>
        /// <returns>(awaitable) Асинхронная задача</returns>
        public async Task SendMessagesAsync(MailRequest mailMessageInfo)
        {
            foreach (var i in mailMessageInfo.Recipients) 
            { 
                try
                {
                    MailAddress address = new(i.Trim());
                    System.Net.Mail.MailMessage message = new(server, address)
                    {
                        Subject = mailMessageInfo.Subject.Trim(),
                        Body = mailMessageInfo.Body.Trim(),
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
}
