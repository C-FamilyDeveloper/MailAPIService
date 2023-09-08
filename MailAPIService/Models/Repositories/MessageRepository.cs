using MailAPIService.Models.DataContexts;
using MailAPIService.Models.DataEntities;
using MailAPIService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MailAPIService.Models.Repositories
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
            await context.Messages.AddAsync(entity);
        }
        public async Task Update(MailMessage entity)
        {
            await Task.Run(()=>context.Messages.Update(entity));
        }

        public async Task Delete(MailMessage entity)
        {
            await Task.Run(() => context.Messages.Remove(entity));
        }


        public async Task<List<MailMessage>> Get()
        {
            return await context.Messages.ToListAsync();
        }
        public async Task<MailMessage?> Find(MailMessage entity)
        {
            var find = await context.Messages.Where(
                i => i.Body == entity.Body && i.Subject == entity.Subject).FirstOrDefaultAsync();
            return (find == default) ? null : find;
        }
        public async Task<MailMessage> AddIfNotExist(MailMessage entity)
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
        public  async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
