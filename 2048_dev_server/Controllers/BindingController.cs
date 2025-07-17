using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2048_dev_server.Models;
using _2048_dev_server.Data;
using System.Collections.Generic;
using System.Linq;

namespace _2048_dev_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BindingController : ControllerBase
    {
        private readonly GameDbContext _db;

        public BindingController(GameDbContext db)
        {
            _db = db;
        }

        [HttpPost("bind")]
        public IActionResult Bind([FromBody] BindRequest request)
        {
            var user = _db.Users
                .Include(u => u.UserIdpBindings)
                .FirstOrDefault(u => u.UserId == request.UserId);

            if (user == null)
                return Unauthorized(new { 
                    error = new ApiError {
                        code = "Unauthorized", 
                        message = "user is null", 
                        field = "UserId"
                    }});

            // 이미 바인딩된 타입이 아니면 추가
            if (!user.UserIdpBindings.Any(b => b.LoginType == request.LoginType))
            {
                user.UserIdpBindings.Add(new UserIdpBinding
                {
                    LoginType = request.LoginType,
                    UserId = user.UserId
                });
                _db.SaveChanges();
            }

            var patch = new Dictionary<string, object>
            {
                { $"{nameof(Models.User.UserIdpBindings)}", user.UserIdpBindings.Select(b => b.LoginType).ToList() }
            };
            
            var result = new Dictionary<string, object> {
                [$"{nameof(Models.User)}."] = patch
            };

            return Ok(new { data = result, ok = true});
        }
        
        [HttpPost("unbind")]
        public IActionResult Unbind([FromBody] BindRequest request)
        {
            var user = _db.Users
                .Include(u => u.UserIdpBindings)
                .FirstOrDefault(u => u.UserId == request.UserId);

            if (user == null)
                return Unauthorized(new { 
                    error = new ApiError {
                        code = "Unauthorized", 
                        message = "user is null", 
                        field = "UserId"
                    }});

            var target = user.UserIdpBindings.FirstOrDefault(b => b.LoginType == request.LoginType);
            if (target != null)
            {
                user.UserIdpBindings.Remove(target);
                _db.SaveChanges();
            }

            var patch = new Dictionary<string, object>
            {
                { $"{nameof(Models.User.UserIdpBindings)}", user.UserIdpBindings.Select(b => b.LoginType).ToList() }
            };

            var result = new Dictionary<string, object> {
                [$"{nameof(Models.User)}."] = patch
            };

            return Ok(new { data = result, ok = true});
        }
    }
}
