﻿using MailerAPIService.Models.DataContexts;
using MailerAPIService.Models.DataEntities;
using MailerAPIService.Models.Interfaces;

namespace MailerAPIService.Models.Repositories
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
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(MessageRecipient entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(MessageRecipient entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }


        public IQueryable<MessageRecipient> GetAllEntities()
        {
            return context.MessagesRecipients;
        }
    }
}