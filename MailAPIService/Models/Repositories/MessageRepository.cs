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
        public async Task<MailMessage?> Find(MailMessage entity)
        {
            await Task.Delay(100);
            var find = context.Messages.Where(
                i => i.Body == entity.Body && i.Subject == entity.Subject).FirstOrDefault();
            return (find == default) ? null : find;
        }
        public async Task<MailMessage> AddIfNotExist(MailMessage entity)
        {
            var find = await Find(entity);
            if (find == null)
            {
                await Add(entity);
                return context.Messages.OrderBy(i => i.Id).Last();
            }
            else
            {
                return find;
            }
        }
    }
}
