using MailAPIService.Models.DataContexts;
using MailAPIService.Models.DataEntities;
using MailAPIService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MailAPIService.Models.Repositories
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
            await context.Recipients.AddAsync(entity);
            //await context.SaveChangesAsync();
        }
        public async Task Update(Recipient entity)
        {
            await Task.Run(() => context.Recipients.Update(entity));
            //await context.SaveChangesAsync();
        }
        public async Task Delete(Recipient entity)
        {
            await Task.Run(() => context.Recipients.Remove(entity));
            //await context.SaveChangesAsync();
        }
        public async Task<List<Recipient>> GetAll()
        {
            return await context.Recipients.ToListAsync();
        }
        public async Task<Recipient?> Find(Recipient entity)
        {
            var find = await context.Recipients.Where(i => i.Email == entity.Email).FirstOrDefaultAsync();
            return (find == default)? null : find;
        }
        public async Task<Recipient> AddIfNotExist(Recipient entity)
        {
            var find = await Find(entity);
            if (find == null)
            {
                await Add(entity);
                return entity;
            }
            else
            {
                return find;
            }
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
