using MailAPIService.Models.Requests;
using MailAPIService.Models.Responces;
using MailAPIService.Models.DataEntities;
using MailAPIService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MailAPIService.Models.Enums;
using MailAPIService.Models.Abstractions;

namespace MailAPIService.Controllers
{
    [ApiController]
    [Route("api/mails")]
    public class MailConroller : ControllerBase
    {
        private readonly IBaseRepository<MailMessage> mailrepository;
        private readonly IBaseRepository<MessageRecipient> messagerecipientrepository;
        private readonly IBaseRepository<Recipient> recipientrepository;
        private readonly IBaseRepository<MailLog> logrepository;
        private readonly IMailService mailService;

        public MailConroller(IBaseRepository<MailMessage> mailrepository,
            IBaseRepository<MessageRecipient> messagerecipientrepository,
            IBaseRepository<Recipient> recipientrepository,
            IBaseRepository<MailLog> logrepository,
            IMailService mailService)
        {
            this.mailrepository = mailrepository;
            this.messagerecipientrepository = messagerecipientrepository;
            this.recipientrepository = recipientrepository;
            this.logrepository = logrepository;
            this.mailService = mailService;
        }

        [HttpGet]
        public async Task<List<MailResponce>>  Get()
        {
            var list = await messagerecipientrepository.Get();
            var responce  = list.Select(i => new MailResponce
            {
                MailDateTime = i.MailLog.MailDateTime,
                Body = i.Message.Body,
                Subject = i.Message.Subject,
                FailedMessage = i.MailLog.FailedMessage,
                MailResult = i.MailLog.MailResult,
            }).ToList();
            return responce;
        }

        [HttpPost]
        public async Task Post([FromBody] MailRequest mailRequest)
        {
            MailMessage message = await mailrepository.AddIfNotExist(new() { Body = mailRequest.Body, Subject = mailRequest.Subject });
            foreach (var i in mailRequest.Recipients)
            {
                MailLog log = new();
                try
                {
                    await mailService.SendMessageAsync(i, mailRequest.Subject, mailRequest.Body);
                    log.MailResult = Result.OK;
                }
                catch (Exception ex)
                {
                    log.MailResult = Result.Failed;
                    log.FailedMessage = ex.Message;
                }
                finally
                {
                    log.MailDateTime = DateTime.Now;
                    Recipient recipient = await recipientrepository.AddIfNotExist( new() { Email = i });
                    log.MailMessageRecipient = new MessageRecipient
                    {
                        Message = message,
                        MailLog = log,
                        MailRecipient = recipient
                    };
                    await logrepository.Add(log);                    
                }
            }
            await logrepository.Save();
        }
    }
}