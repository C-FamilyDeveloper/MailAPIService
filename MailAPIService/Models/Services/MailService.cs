using System.ComponentModel.DataAnnotations;
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
                Timeout = 1000,
                EnableSsl = true
            };
        }
        /// <summary>
        /// Отправка сообщения по электронной почте, по заданному адресу
        /// </summary>
        /// <param name="email">Адрес электронной почты получателя</param>
        /// <param name="subject">Тема сообщения</param>
        /// <param name="body">Тело сообщения</param>
        /// <returns>(awaitable) Асинхронная задача</returns>
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
