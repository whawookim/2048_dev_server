namespace _2048_dev_server.Models;

public class LoginRequest
{
    /// <summary>
    /// <see cref="LoginType"/>
    /// </summary>
    public string? LoginType { get; set; }
    /// <summary>
    /// UserId
    /// </summary>
    public string? UserId { get; set; }
    /// <summary>
    /// Guest의 경우 null로 들어옴.
    /// </summary>
    public string? Token { get; set; }
}
