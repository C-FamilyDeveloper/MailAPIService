using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailAPIService.Models.DataEntities
{
    public class Recipient
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Ссылка на записи из зависимой таблицы MessageRecipients
        /// </summary>
        public List<MessageRecipient> MessageRecipients { get; set; } = new();
    }
}
