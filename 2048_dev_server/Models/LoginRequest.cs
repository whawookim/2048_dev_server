namespace _2048_dev_server.Models;

public class LoginRequest
{
    public string LoginType { get; set; } // "Guest" or "Google"
    public string UserId { get; set; }
    public string Token { get; set; }
}
