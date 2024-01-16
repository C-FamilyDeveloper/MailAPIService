using MailAPIService.Models.Interfaces;
using MailAPIService.Models.Services.CRUD;

namespace MailAPIService.Models.Facades
{
    /// <summary>
    /// Фасад, принимающий две инъекции : CRUD сервис и сервис отправки сообщений
    /// </summary>
    public class MailFacade
    {
        private readonly IMailService mailService;
        private readonly IMailLogService mailLogService;

        public MailFacade(IMailService mailService, IMailLogService mailLogService)
        {
            this.mailService = mailService;
            this.mailLogService = mailLogService;
        }

        /// <summary>
        /// Отправка сообщения по электронной почте, по заданному адресу
        /// </summary>
        /// <param name="message">класс  ViewModel данных отправки</param>
        /// <returns>(awaitable) Асинхронная задача</returns>
        public async Task SendMail(SendingMessage message)
        {
            SendingResult result = await mailService.TrySendMessage(message);
            await mailLogService.LogMailMessage(message, result);
        }

    }
}
