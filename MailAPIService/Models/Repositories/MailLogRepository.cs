using MailerAPIService.Models.DataContexts;
using MailerAPIService.Models.DataEntities;
using MailerAPIService.Models.Interfaces;

namespace MailerAPIService.Models.Repositories
{
    public class MailLogRepository : IBaseRepository<MailLog>
    {
        private ApplicationContext context;
        public MailLogRepository (ApplicationContext context)
        {
            this.context = context;
        }
        public async Task Add(MailLog entity)
        { 
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(MailLog entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(MailLog entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }


        public IQueryable<MailLog> GetAllEntities()
        {
            return context.Logs;
        }

        public async Task<MailLog?> Find(MailLog entity)
        {
            var findedentity = await context.Logs.FindAsync(entity);
            return findedentity;
        }

        public async Task<MailLog> AddIfNotExist(MailLog entity)
        {
            var find = await Find(entity);
            if (find == null)
            {
                await Add(entity);
                return context.Logs.OrderBy(i=>i.Id).Last();
            }
            else
            {
                return find;
            }           
        }
    }
}
