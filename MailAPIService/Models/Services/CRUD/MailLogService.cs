using MailAPIService.Models.DataContexts;
using MailAPIService.Models.Interfaces;
using MailAPIService.Models.Responces;
using Microsoft.EntityFrameworkCore;

namespace MailAPIService.Models.Services.CRUD
{
    public class MailLogService : IMailLogService
    {
        private readonly ApplicationContext applicationContext;

        public MailLogService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task LogMailMessage(SendingMessage message, SendingResult result)
        {
            await applicationContext.Logs.AddAsync(new DataEntities.MailLog
            {
                MailDateTime = result.DateTime,
                MailResult = result.Result,
                FailedMessage = (result.Result == Enums.Result.OK) ? null : result.ErrorMessage,
                MessageRecipient = new DataEntities.MessageRecipient
                {
                    Message = new DataEntities.MailMessage
                    {
                        Body = message.Body,
                        Subject = message.Subject
                    },
                    MailRecipient = new DataEntities.Recipient
                    {
                        Email = message.EMail
                    }
                }
            });

            await applicationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MailResponce>> GetLogs()
        {
            return await applicationContext.Logs.AsNoTracking().Select(i => new MailResponce
            {
                FailedMessage = i.FailedMessage,
                MailDateTime = i.MailDateTime,
                Body = i.MessageRecipient.Message.Body,
                MailResult = i.MailResult,
                RecipientEmail = i.MessageRecipient.MailRecipient.Email,
                Subject = i.MessageRecipient.Message.Subject
            }).ToListAsync();
        }
    }
}
