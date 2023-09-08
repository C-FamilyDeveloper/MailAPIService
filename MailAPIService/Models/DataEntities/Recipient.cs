using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MailAPIService.Models.DataEntities
{
    public class Recipient
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<MessageRecipient> MessageRecipients { get; set; } = new();
    }
}
