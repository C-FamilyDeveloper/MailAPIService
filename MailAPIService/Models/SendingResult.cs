using MailAPIService.Models.Enums;

namespace MailAPIService.Models
{
    public class SendingResult
    {
        public Result Result { get; set; }
        public DateTime DateTime { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
