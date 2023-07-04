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
        private readonly IBaseRepository<MailMessage> mailrepository;
        private readonly IBaseRepository<Recipient> recipientrepository;
        public MailConroller(ILogger<MailMessage> logger, IBaseRepository<MailMessage> mailrepository,
            IBaseRepository<Recipient> recipientrepository)
        {
            this.logger = logger;
            this.mailrepository = mailrepository;
            this.recipientrepository = recipientrepository;
        }
        /// <summary>
        /// GET ������ � ���� "api/mails, �������� ���������� � ���������� 
        /// </summary>
        /// <returns>JSON ���� � �������</returns>
        [HttpGet]
        public List<MailResponce>  Get()
        {
            return mailrepository.GetAllEntities().Select(i => new MailResponce
            {
                MailDateTime = i.MailLog.MailDateTime,
                Body = i.Body,
                Subject = i.Subject,
                FailedMessage = i.MailLog.FailedMessage,
                Result = i.MailLog.Result,
            }).ToList();
        }
        /// <summary>
        /// POST ������ � ���� "api/mails, c JSON �����������, ���������� ��������� ��������� �  �������� �� 
        /// </summary>
        /// <param name="mailInfo">JSON ���� � ����������� � ����������� � ���������</param>
        /// <returns>(awaitable) ����������� ������</returns>
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
                await mailrepository.Add(message);
                message.MessageRecipients = mailRequest.Recipients.Select(i =>
                    new MessageRecipient
                    {
                        Message = message,
                        MailRecipient =  recipientrepository.GetAllEntities().Select(j=>
                        j.Email.Trim()).Contains(i)? 
                            recipientrepository.GetAllEntities().Where(k=>k.Email.Trim() == i).First() 
                            :
                            new Recipient { Email = i }
                    }).ToList();
                await mailrepository.Update(message);
            }
        }
    }
}