using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailUpdateController : ControllerBase
{
    private readonly ILogger<EmailUpdateController> _logger;

    public EmailUpdateController(ILogger<EmailUpdateController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetEmails")]
    public IActionResult Get()
    {
        var authResult = AuthenticationHelper.AuthenticateRequest(Request);

        if (authResult.GetType() != typeof(OkResult)) {
            return authResult;
        }

        DatabaseIO db = new();
        var emails = db.GetEmails();

        return Ok(emails);
    }

    [HttpPut(Name = "AddEmail")]
    public IActionResult Put([FromBody] ClientUpdateModel body)
    {

        var authResult = AuthenticationHelper.AuthenticateRequest(Request);

        if (authResult.GetType() != typeof(OkResult)) {
            return authResult;
        }

        var email = new EmailAddressAttribute();

        if (body is null || body.Email is null) {
            return BadRequest("Request body invalid");
        }
        if (!email.IsValid(body.Email)) {
            return BadRequest("Not a valid Email");
        }

        DatabaseIO db = new();
        db.AddEmailToDb(body.Email);

        return Ok("Success");

    }

    [HttpDelete(Name = "RemoveEmail")]
    public IActionResult Delete([FromBody] ClientUpdateModel body)
    {
        var authResult = AuthenticationHelper.AuthenticateRequest(Request);

        if (authResult.GetType() != typeof(OkResult)) {
            return authResult;
        }

        var email = new EmailAddressAttribute();

        if (body is null || body.Email is null) {
            return BadRequest("Request body invalid");
        }
        if (!email.IsValid(body.Email)) {
            return BadRequest("Not a valid Email");
        }

        DatabaseIO db = new();
        db.RemoveEmailFromDB(body.Email);

        return Ok("Success");

    }

    [HttpPost(Name = "DumpEmails")]
    public IActionResult Post([FromBody] ClientUpdateModel body)
    {
        var authResult = AuthenticationHelper.AuthenticateRequest(Request);

        if (authResult.GetType() != typeof(OkResult)) {
            return authResult;
        }
        
        DatabaseIO db = new();
        // var emails = db.GetEmails();
        db.DumpToEmailsDB();

        return Ok();
    }
}
