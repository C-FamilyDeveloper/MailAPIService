using MailerAPIService.Models.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace MailerAPIService.Models.DataContexts
{
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Таблица получателей
        /// </summary>
        public DbSet<Recipient> Recipients { get; set; } = null!;
        /// <summary>
        /// Таблица сообщений
        /// </summary>
        public DbSet<MailMessage> Messages { get; set; } = null!;
        /// <summary>
        /// Таблица логов
        /// </summary>
        public DbSet<MailLog> Logs { get; set; } = null!;
        /// <summary>
        /// Таблица получателей и сообщений
        /// </summary>
        public DbSet<MessageRecipient> MessagesRecipients { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
        }
    }
}
