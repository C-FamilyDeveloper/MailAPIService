using MailAPIService.Models.Requests;
using MailAPIService.Models.Responces;
using MailAPIService.Models.DataEntities;
using MailAPIService.Models.Interfaces;
using MailAPIService.Models.Services;
using Microsoft.AspNetCore.Mvc;
using MailAPIService.Models.Enums;
using Microsoft.Extensions.Options;
using MailAPIService.Models.Configs;

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
        private readonly IOptions <Config> options;

        public MailConroller(IBaseRepository<MailMessage> mailrepository,
            IBaseRepository<MessageRecipient> messagerecipientrepository,
            IBaseRepository<Recipient> recipientrepository,
            IBaseRepository<MailLog> logrepository,
            IOptions<Config> options)
        {
            this.mailrepository = mailrepository;
            this.messagerecipientrepository = messagerecipientrepository;
            this.recipientrepository = recipientrepository;
            this.logrepository = logrepository;
            this.options = options;
        }
        /// <summary>
        /// GET запрос к пути "api/mails, получает информацию о сообщениях 
        /// </summary>
        /// <returns>(awaitable) Асинхронная задача c JSON файлом ответа </returns>
        [HttpGet]
        public async Task<List<MailResponce>>  Get()
        {
            var list = await messagerecipientrepository.GetAll();
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
        /// <summary>
        /// POST запрос к пути "api/mails, c JSON параметрами, отправляет адресатам сообщения и  логирует их 
        /// </summary>
        /// <param name="mailRequest">JSON файл с информацией о получателях и сообщении</param>
        /// <returns>(awaitable) Асинхронная задача</returns>
        [HttpPost]
        public async Task Post([FromBody] MailRequest mailRequest)
        {
            var service = new MailService(options.Value.MailServerInfo);
            MailMessage message = await mailrepository.AddIfNotExist(new() { Body = mailRequest.Body, Subject = mailRequest.Subject });
            foreach (var i in mailRequest.Recipients)
            {
                MailLog log = new();
                try
                {
                    await service.SendMessageAsync(i, mailRequest.Subject, mailRequest.Body);
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