using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailerAPIService.Models.DataEntities
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
        public string Result { get; set; }
        /// <summary>
        /// Текст ошибки (если есть)
        /// </summary>
        public string FailedMessage { get; set; }
        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime MailDateTime { get; set; }
        /// <summary>
        /// Ссылка на запись сообщения
        /// </summary>
        public int MessageId { get; set; }
        [ForeignKey(nameof(MessageId))]
        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public MailMessage MailMessage { get; set; }
    }
}
