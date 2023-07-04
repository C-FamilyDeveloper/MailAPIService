using System.Text.Json.Serialization;

namespace MailAPIService.Models.Responces
{
    [Serializable]
    public class MailResponce
    {
        /// <summary>
        /// Тема сообщения
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; }
        /// <summary>
        /// Дата и время отправки сообщения
        /// </summary>
        [JsonPropertyName("datetime")]
        public DateTime MailDateTime { get; set; }
        /// <summary>
        /// Результат отправки сообщения
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
        /// <summary>
        /// Дата и время отправки сообщения
        /// </summary>
        [JsonPropertyName("failedmessage")]
        public string FailedMessage { get; set; }
    }
}
