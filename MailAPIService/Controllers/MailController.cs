using MailAPIService.Models.Facades;
using MailAPIService.Models.Interfaces;
using MailAPIService.Models.Requests;
using MailAPIService.Models.Responces;
using MailAPIService.Models.Services.CRUD;
using Microsoft.AspNetCore.Mvc;

namespace MailAPIService.Controllers
{
    [ApiController]
    [Route("api/mails")]
    public class MailConroller : ControllerBase
    {
        private readonly MailFacade mailFacade;
        private readonly IMailLogService mailService;

        public MailConroller(MailFacade mailFacade, IMailLogService mailService)
        {
            this.mailFacade = mailFacade;
            this.mailService = mailService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MailResponce>>> Get()
        {
            try
            {
                return Ok(await mailService.GetLogs());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MailRequest mailRequest)
        {
            try
            {
                foreach (var recipient in mailRequest.Recipients)
                {
                    await mailFacade.SendMail(new Models.SendingMessage
                    {
                        Body = mailRequest.Body,
                        EMail = recipient,
                        Subject = mailRequest.Subject
                    });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}