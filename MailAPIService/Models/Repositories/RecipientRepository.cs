using MailerAPIService.Models.DataContexts;
using MailerAPIService.Models.DataEntities;
using MailerAPIService.Models.Interfaces;

namespace MailerAPIService.Models.Repositories
{
    public class RecipientRepository : IBaseRepository<Recipient>
    {
        private ApplicationContext context;
        public RecipientRepository (ApplicationContext context)
        {
            this.context = context;
        }
        public async Task Add(Recipient entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(Recipient entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Recipient entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }


        public IQueryable<Recipient> GetAllEntities()
        {
            return context.Recipients;
        }
    }
}
