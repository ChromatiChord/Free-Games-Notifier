using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private IDatabaseIO _dbIO = DataIOFactory.DatabaseIOCreate();

        public EmailController(ILogger<EmailController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetEmails", Name = "GetEmails")]
        [RequireAdminAuth]
        public IActionResult Get()
        {
            var emails = _dbIO.GetEmails();
            return Ok(emails);
        }

        [HttpPut("AddEmail", Name = "AddEmail")]
        [RequireAuth]
        [ValidateEmail]
        public IActionResult Put([FromBody] ClientUpdateModel body)
        {            
            _dbIO.AddEmailToDb(body.Email);

            return Ok("Success");
        }

        [HttpDelete("RemoveEmail", Name = "RemoveEmail")]
        [RequireAdminAuth]
        [ValidateEmail]
        public IActionResult Delete([FromBody] ClientUpdateModel body)
        {            
            _dbIO.RemoveEmailFromDB(body.Email);

            return Ok("Success");
        }

        [HttpPost("DumpEmails", Name = "DumpEmails")]
        [RequireAdminAuth]
        public IActionResult Post()
        {        
            _dbIO.DumpToEmailsDB();

            return Ok();
        }
    }
}
