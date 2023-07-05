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
        public async Task<Recipient?> Find(Recipient entity)
        {
            await Task.Delay(100);
            var find = context.Recipients.Where(i => i.Email == entity.Email).FirstOrDefault();
            return (find == default)? null : find;
        }
        public async Task<Recipient> AddIfNotExist(Recipient entity)
        {
            var find = await Find(entity);
            if (find == null)
            {
                await Add(entity);
                return context.Recipients.OrderBy(i => i.Id).Last();
            }
            else
            {
                return find;
            }
        }
    }
}
