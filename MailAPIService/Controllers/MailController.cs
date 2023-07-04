using MailAPIService.Models.Requests;
using MailAPIService.Models.Responces;
using MailerAPIService.Models.DataEntities;
using MailerAPIService.Models.Interfaces;
using MailerAPIService.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailAPIService.Controllers
{
    [ApiController]
    [Route("api/mails")]
    public class MailConroller : ControllerBase
    {
        private readonly ILogger<MailMessage> logger;
        private readonly IBaseRepository<MailMessage> repository;

        public MailConroller(ILogger<MailMessage> logger, IBaseRepository<MailMessage> repository)
        {
            this.logger = logger;
            this.repository = repository;
        }
        /// <summary>
        /// GET запрос к пути "api/mails, получает информацию о сообщениях 
        /// </summary>
        /// <returns>JSON файл с ответом</returns>
        [HttpGet]
        public List<MailResponce>  Get()
        {
            return repository.GetAllEntities().Select(i => new MailResponce
            {
                MailDateTime = i.MailLog.MailDateTime,
                Body = i.Body,
                Subject = i.Subject,
                FailedMessage = i.MailLog.FailedMessage,
                Result = i.MailLog.Result,
            }).ToList();
        }
        /// <summary>
        /// POST запрос к пути "api/mails, c JSON параметрами, отправляет адресатам сообщения и  логирует их 
        /// </summary>
        /// <param name="mailInfo">JSON файл с информацией о получателях и сообщении</param>
        /// <returns>(awaitable) Асинхронная задача</returns>
        [HttpPost]
        public async Task Post([FromBody] MailRequest mailRequest)
        {
            var service = new MailService(ConfigService.GetServerAuthFromConfig(
                "C:\\Users\\User\\Downloads\\config.ini"));
            MailLog log = new();
            try
            {
                await service.SendMessagesAsync(mailRequest);
                log.Result = "OK";
                log.FailedMessage = "";
            }
            catch (Exception ex)
            {
                log.Result = "Failed";
                log.FailedMessage = ex.Message;
            }
            finally
            {
                log.MailDateTime = DateTime.Now;
                var message = new MailMessage
                {
                    MailLog = log,
                    Body = mailRequest.Body,
                    Subject = mailRequest.Subject
                };
                await repository.Add(message);
                message.MessageRecipients = mailRequest.Recipients.Select(i =>
                    new MessageRecipient
                    {
                        Message = message,
                        MailRecipient = new Recipient { Email = i }
                    }).ToList();
                await repository.Update(message);
            }
        }
    }
}