using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MailerAPIService.Models.DataEntities
{
    public class MailMessage
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Тема сообщения
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Ссылка на записи из зависимой таблицы MessageRecipients
        /// </summary>
        public List<MessageRecipient> MessageRecipients { get; set; } = new();
    }
}
