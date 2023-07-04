using Microsoft.AspNetCore.Mvc;

namespace GamesNotifierApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IDatabaseIO _dbIO = DataIOFactory.DatabaseIOCreate();

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetUsers", Name = "GetUsers")]
        [RequireAdminAuth]
        public IActionResult Get()
        {
            var emails = _dbIO.GetUsers();
            return Ok(emails);
        }

        [HttpPut("AddUser", Name = "AddUser")]
        [ValidateEmail]
        public IActionResult Put([FromBody] UserUpdateRequestModel body)
        {            
            if (_dbIO.EmailExists(body.Email).Result)
                return Conflict("Email already exists in DB");

            if (body.Services.Count == 0)
                return BadRequest("No services provided");

            if (_dbIO.GetAllUserEmails().Result.Count >= 20)
                return BadRequest("Too many users in DB, try again later");
            
            _dbIO.AddUserToDb(body.Email, body.Services);

            return Ok("Success");
        }

        [HttpDelete("RemoveUser/{uuid}", Name = "RemoveUser")]
        public IActionResult Delete(string uuid)
        {    
            if (!_dbIO.UserExists(uuid).Result)
                return NotFound("User not found in DB");

            _dbIO.RemoveUserFromDB(uuid);

            return Ok("Success");
        }

        [HttpPost("DumpUserDB", Name = "DumpUserDB")]
        [RequireAdminAuth]
        public IActionResult Post()
        {        
            _dbIO.DumpToUsersDB();

            return Ok();
        }
    }
}
