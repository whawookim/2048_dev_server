using Microsoft.AspNetCore.Mvc;
using _2048_dev_server.Models;

namespace _2048_dev_server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRequest request)
    {
        Console.WriteLine($"UUID: {request.Uuid}, Nickname: {request.Nickname}");
        return Ok(new { message = "Registered", request.Uuid, request.Nickname });
    }
}
