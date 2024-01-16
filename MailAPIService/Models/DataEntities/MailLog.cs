using MailAPIService.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MailAPIService.Models.DataEntities
{
    public class MailLog
    {
        /// <summary>
        /// Первичный ключ 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Код сообщения
        /// </summary>
        public Result MailResult { get; set; }
        /// <summary>
        /// Текст ошибки (если есть)
        /// </summary>
        [MaybeNull]
        public string? FailedMessage { get; set; }
        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime MailDateTime { get; set; }
        public int MessageRecipientId { get; set; }
        /// <summary>
        /// Ссылка на таблицу истории
        /// </summary>
        [ForeignKey(nameof(MessageRecipientId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public MessageRecipient MessageRecipient { get; set; }
    }
}
