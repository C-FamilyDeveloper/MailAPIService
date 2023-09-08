using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MailAPIService.Models.DataEntities
{
    public class MailMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Body { get; set; }

        public string Subject { get; set; }

        public List<MessageRecipient> MessageRecipients { get; set; } = new();
    }
}
