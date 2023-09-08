using MailAPIService.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MailAPIService.Models.DataEntities
{
    public class MailLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Result MailResult { get; set; }

        [MaybeNull]
        public string? FailedMessage { get; set; }

        public DateTime MailDateTime { get; set; }

        public int MessageRecipientId { get; set; }

        [ForeignKey(nameof(MessageRecipientId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public MessageRecipient MailMessageRecipient { get; set; }
    }
}
