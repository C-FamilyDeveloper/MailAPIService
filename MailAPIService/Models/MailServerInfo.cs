namespace MailerAPIService.Models
{
    public class MailServerInfo
    {
        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string ServerAddress { get; set; }
        /// <summary>
        /// Аутентификационный ключ или пароль аккаунта
        /// </summary>
        public string AuthPassword { get; set; }
        /// <summary>
        /// SMTP хост отправителя
        /// </summary>
        public string SMTPHost { get; set; }
        /// <summary>
        /// SMTP порт отправителя
        /// </summary>
        public int SMTPPort { get; set; }
        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string DisplayName { get; set; }
    }
}
