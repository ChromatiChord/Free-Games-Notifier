using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;

    public EmailController(ILogger<EmailController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetEmails", Name = "GetEmails")]
    [RequireAuth]
    public IActionResult Get()
    {
        DatabaseIO db = new();
        var emails = db.GetEmails();

        return Ok(emails);
    }

    [HttpPut("AddEmail", Name = "AddEmail")]
    [RequireAuth]
    [ValidateEmail]
    public IActionResult Put([FromBody] ClientUpdateModel body)
    {
        DatabaseIO db = new();
        db.AddEmailToDb(body.Email);

        return Ok("Success");

    }

    [HttpDelete("RemoveEmail", Name = "RemoveEmail")]
    [RequireAuth]
    [ValidateEmail]
    public IActionResult Delete([FromBody] ClientUpdateModel body)
    {

        DatabaseIO db = new();
        db.RemoveEmailFromDB(body.Email);

        return Ok("Success");

    }

    [HttpPost("DumpEmails", Name = "DumpEmails")]
    [RequireAuth]
    public IActionResult Post([FromBody] ClientUpdateModel body)
    {        
        DatabaseIO db = new();
        // var emails = db.GetEmails();
        db.DumpToEmailsDB();

        return Ok();
    }
}
