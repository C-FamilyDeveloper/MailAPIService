using MailAPIService.Models.DataContexts;
using MailAPIService.Models.DataEntities;
using MailAPIService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MailAPIService.Models.Repositories
{
    public class MessageRecipientRepository : IBaseRepository<MessageRecipient>
    {
        private ApplicationContext context;
        public MessageRecipientRepository (ApplicationContext context)
        {
            this.context = context;
        }
        public async Task Add(MessageRecipient entity)
        {
            await context.MessagesRecipients.AddAsync(entity);
            //await context.SaveChangesAsync();
        }
        public async Task Update(MessageRecipient entity)
        {
            await Task.Run(() => context.MessagesRecipients.Update(entity));
            //await context.SaveChangesAsync();
        }

        public async Task Delete(MessageRecipient entity)
        {
            await Task.Run(() => context.MessagesRecipients.Remove(entity));
            //await context.SaveChangesAsync();
        }


        public async Task<List<MessageRecipient>> GetAll()
        {
            return await context.MessagesRecipients.Include(
                i=>i.MailLog).Include(
                    j=>j.MailRecipient).Include(
                        k=>k.Message).ToListAsync();
        }
        public async Task<MessageRecipient?> Find(MessageRecipient entity)
        {
            return await context.MessagesRecipients.FindAsync(entity);
        }
        public async Task<MessageRecipient> AddIfNotExist(MessageRecipient entity)
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
