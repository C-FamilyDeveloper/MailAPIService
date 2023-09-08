using System.ComponentModel.DataAnnotations;

namespace MailAPIService.Models.Abstractions
{
    public interface IMailService
    {
        Task SendMessageAsync([EmailAddress] string email, string subject, string body);
    }
}
