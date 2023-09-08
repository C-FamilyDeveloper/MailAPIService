using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MailAPIService.Models.DataEntities
{
    public class MessageRecipient
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MessageId { get; set; }

        public int RecipientId { get; set; }

        [ForeignKey(nameof(MessageId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public MailMessage Message { get; set; }

        [ForeignKey(nameof(RecipientId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public Recipient MailRecipient { get; set; }

        public MailLog MailLog { get; set; }
    }
}
