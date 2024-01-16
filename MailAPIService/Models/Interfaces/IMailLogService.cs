using MailAPIService.Models.Responces;

namespace MailAPIService.Models.Interfaces
{
    /// <summary>
    /// CRUD сервис логгирования данных в БД
    /// </summary>
    public interface IMailLogService
    {
        /// <summary>
        /// Логгирование результата отправки сообщения 
        /// </summary>
        /// <param name="message">класс с ViewModel данных отправки</param>
        /// <param name="result"> класс с ViewModel результата отправки</param>
        /// <returns>(awaitable) Асинхронная задача</returns>
        Task LogMailMessage(SendingMessage message, SendingResult result);
        /// <summary>
        /// Получение списка логов
        /// </summary>
        /// <returns>(awaitable) Асинхронная задача, возвращающая списка ViewModel </returns>
        Task<IEnumerable<MailResponce>> GetLogs();
    }
}
