using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2048_dev_server.Models;
using _2048_dev_server.Data;

namespace _2048_dev_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NicknameController : ControllerBase
    {
        private readonly GameDbContext _db;

        public NicknameController(GameDbContext db)
        {
            _db = db;
        }
        
        [HttpPost("change-nickname")]
        public IActionResult ChangeNickname([FromBody] ChangeNicknameRequest request)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserId == request.UserId);

            if (user == null)
                return Unauthorized(new { 
                    error = new ApiError {
                        code = "Unauthorized", 
                        message = "user is null", 
                        field = "Nickname"
                    }});

            user.NickName = request.NewNickname;
            _db.SaveChanges();

            var patch = new Dictionary<string, object>
            {
                { $"{nameof(Models.User.NickName)}", user.NickName }
            };
            
            var result = new Dictionary<string, object> {
                [$"{nameof(Models.User)}."] = patch
            };

            return Ok(new { data = result, ok = true});
        }
    }    
}
