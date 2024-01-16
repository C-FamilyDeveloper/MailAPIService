using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailAPIService.Models.DataEntities
{
    public class MessageRecipient
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Внешний ключ Message
        /// </summary>
        public int MessageId { get; set; }
        /// <summary>
        /// Внешний ключ Recipient
        /// </summary>
        public int RecipientId { get; set; }
        /// <summary>
        /// Ссылка на сообщение
        /// </summary>
        [ForeignKey(nameof(MessageId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public MailMessage Message { get; set; }
        /// <summary>
        /// Ссылка на  получателя
        /// </summary>
        [ForeignKey(nameof(RecipientId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public Recipient MailRecipient { get; set; }
        /// <summary>
        /// Ссылка на лог сообщения
        /// </summary>
        public MailLog Log { get; set; }

    }
}
