using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2048_dev_server.Models;
using _2048_dev_server.Data;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace _2048_dev_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly GameDbContext _db;

        public LoginController(GameDbContext db)
        {
            _db = db;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            return request.LoginType switch
            {
                nameof(LoginType.Guest) => HandleGuestLogin(request),
                nameof(LoginType.Google) => HandleGoogleLogin(request),
                _ => BadRequest("Unsupported login type")
            };
        }

        private IActionResult HandleGuestLogin(LoginRequest request)
        {
            // 예시: 토큰 검증 없이 UserId로 조회
            var user = _db.Users
                .Include(u => u.UserIdpBindings)
                .FirstOrDefault(u => u.UserId == request.UserId);

            if (user == null)
            {
                // 새 유저 생성
                user = new User
                {
                    UserId = request.UserId,
                    NickName = "Guest" + request.UserId[^4..],
                    UserIdpBindings = new List<UserIdpBinding>
                    {
                        new UserIdpBinding { LoginType = nameof(LoginType.Guest) }
                    }
                };
                _db.Users.Add(user);
                _db.SaveChanges();
            }

            return Ok(new Dictionary<string, object> {
                { $"{nameof(Models.User)}", AutoConvert.ToDictionary(user) }
            });
        }

        private IActionResult HandleGoogleLogin(LoginRequest request)
        {
            // 예시: 구글 토큰 검증 생략 (토큰값으로 사용자 ID 매핑)
            var user = _db.Users
                .Include(u => u.UserIdpBindings)
                .FirstOrDefault(u => u.UserId == request.UserId);

            if (user == null)
            {
                // 새 유저 생성
                user = new User
                {
                    UserId = request.UserId,
                    NickName = "Google" + request.UserId[^4..],
                    UserIdpBindings = new List<UserIdpBinding>
                    {
                        new UserIdpBinding { LoginType = nameof(LoginType.Google) }
                    }
                };
                _db.Users.Add(user);
                _db.SaveChanges();
            }

            return Ok(new Dictionary<string, object> {
                { $"{nameof(Models.User)}", AutoConvert.ToDictionary(user) }
            });
        }
    }
}
