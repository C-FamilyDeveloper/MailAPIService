namespace MailAPIService.Models.Interfaces
{
    /// <summary>
    /// Сервис отправки сообщений по электронной почте
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Отправка сообщения по электронной почте, по заданному адресу
        /// </summary>
        /// <param name="message">класс с ViewModel данных отправки</param>
        /// <returns>(awaitable) Асинхронная задача, возвращающая класс со статусным кодом ошибки и возможное сообщение об ошибке</returns>
        Task<SendingResult> TrySendMessage(SendingMessage message);
    }
}
