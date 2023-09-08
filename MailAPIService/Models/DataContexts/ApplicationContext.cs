using MailAPIService.Models.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace MailAPIService.Models.DataContexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Recipient> Recipients { get; set; } = null!;
        public DbSet<MailMessage> Messages { get; set; } = null!;
        public DbSet<MailLog> Logs { get; set; } = null!;
        public DbSet<MessageRecipient> MessagesRecipients { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
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
