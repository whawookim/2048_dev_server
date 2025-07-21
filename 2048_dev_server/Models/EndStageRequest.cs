namespace _2048_dev_server.Models;

public class EndStageRequest
{
    public string UserId { get; set; } = default!;
    public string NickName { get; set; } = default!;
    public string Mode { get; set; } = default!;
    public int Score { get; set; }
    public int ClearTime { get; set; }
    public int MoveCount { get; set; }
}
