using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2048_dev_server.Models;
using _2048_dev_server.Data;

namespace _2048_dev_server.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly GameDbContext _db;

        public LoginController(GameDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            Console.WriteLine($"[로그인 요청] LoginType: {request.LoginType}, UserId: {request.UserId}, Token: {request.Token}");

            return request.LoginType switch
            {
                nameof(LoginType.Guest) => HandleGuestLogin(request),
                nameof(LoginType.Google) => HandleGoogleLogin(request),
                _ => BadRequest(new { 
                        error = new ApiError {
                            code = "InvalidParameter", 
                            message = "Unsupported login type", 
                            field = "LoginType"
                        }})
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

            var responseDict = AutoConvert.ToDictionary(user);

            // 클라이언트 호환을 위해 가볍게 가공
            responseDict[nameof(Models.User.UserIdpBindings)] = user.UserIdpBindings.Select(b => b.LoginType).ToList();

            var result = new Dictionary<string, object> {
                [nameof(Models.User)] = responseDict
            };
            
            return Ok(new { data = result, ok = true});
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

            var responseDict = AutoConvert.ToDictionary(user);

            // 클라이언트 호환을 위해 가볍게 가공
            responseDict[nameof(Models.User.UserIdpBindings)] = user.UserIdpBindings.Select(b => b.LoginType).ToList();

            var result = new Dictionary<string, object> {
                [nameof(Models.User)] = responseDict
            };
            
            return Ok(new { data = result, ok = true });
        }
    }
}
