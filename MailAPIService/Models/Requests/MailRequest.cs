using System.Text.Json.Serialization;

namespace MailAPIService.Models.Requests
{
    [Serializable]
    public class MailRequest
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
        /// Список EMail адресов получателей
        /// </summary>
        [JsonPropertyName("recipients")]
        public string[] Recipients { get; set; }
    }
}
