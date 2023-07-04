using MailerAPIService.Models.DataContexts;
using MailerAPIService.Models.DataEntities;
using MailerAPIService.Models.Interfaces;

namespace MailerAPIService.Models.Repositories
{
    public class MessageRepository : IBaseRepository<MailMessage>
    {
        private ApplicationContext context;
        public MessageRepository (ApplicationContext context)
        {
            this.context = context;
        }
        public async Task Add(MailMessage entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(MailMessage entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(MailMessage entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }


        public IQueryable<MailMessage> GetAllEntities()
        {
            return context.Messages;
        }
    }
}
