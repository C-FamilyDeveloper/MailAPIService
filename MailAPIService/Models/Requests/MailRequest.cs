﻿using System.Text.Json.Serialization;

namespace MailAPIService.Models.Requests
{
    [Serializable]
    public class MailRequest
    {

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("recipients")]
        public string[] Recipients { get; set; }
    }
}
