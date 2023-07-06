using MailAPIService.Models.DataContexts;
using MailAPIService.Models.DataEntities;
using MailAPIService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MailAPIService.Models.Repositories
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
            await context.Logs.AddAsync(entity);
            //await context.SaveChangesAsync();
        }
        public async Task Update(MailLog entity)
        {
            await Task.Run(()=>context.Logs.Update(entity));
            //await context.SaveChangesAsync();
        }

        public async Task Delete(MailLog entity)
        {
            await Task.Run(() => context.Logs.Remove(entity));
            //await context.SaveChangesAsync();
        }


        public async Task<List<MailLog>> GetAll()
        {
            return await context.Logs.ToListAsync();
        }

        public async Task<MailLog?> Find(MailLog entity)
        {
            return await context.Logs.FindAsync(entity);
        }

        public async Task<MailLog> AddIfNotExist(MailLog entity)
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
