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
        [RequireAuth]
        [ValidateEmail]
        public IActionResult Put([FromBody] UserUpdateRequestModel body)
        {            
            if (_dbIO.EmailExists(body.Email).Result)
                return Conflict("Email already exists in DB");

            if (body.Services.Count == 0)
                return BadRequest("No services provided");
            
            _dbIO.AddUserToDb(body.Email, body.Services);

            return Ok("Success");
        }

        [HttpDelete("RemoveUser", Name = "RemoveUser")]
        [RequireAdminAuth]
        public IActionResult Delete([FromBody] RemoveUserModel body)
        {            
            _dbIO.RemoveUserFromDB(body.Uuid);

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
