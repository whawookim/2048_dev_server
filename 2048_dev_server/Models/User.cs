namespace _2048_dev_server.Models;

public enum LoginType
{
    Guest,
    Google,
    Apple,
    // 추가 가능
}

public class User
{
    public string UserId { get; set; }
    public string NickName { get; set; } = "User";
    public List<UserIdpBinding> UserIdpBindings { get; set; } = new(); // RDBMS라 이렇게 함..
}

public class UserIdpBinding
{
    public int Id { get; set; }
    public string UserId { get; set; }  // FK to User.Id
    public string LoginType { get; set; }  // "Google", "Guest"
    public User User { get; set; }
}

public class UserRequest
{
    public string Uuid { get; set; }
    public string Nickname { get; set; }
}
