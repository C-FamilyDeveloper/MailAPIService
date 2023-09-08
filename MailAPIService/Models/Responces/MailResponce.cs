using MailAPIService.Models.Enums;
using System.Text.Json.Serialization;

namespace MailAPIService.Models.Responces
{
    [Serializable]
    public class MailResponce
    {

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("datetime")]
        public DateTime MailDateTime { get; set; }

        [JsonPropertyName("result")]
        public Result MailResult { get; set; }

        [JsonPropertyName("failedmessage")]
        public string? FailedMessage { get; set; }
    }
}
