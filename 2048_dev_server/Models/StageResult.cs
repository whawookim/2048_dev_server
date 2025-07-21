namespace _2048_dev_server.Models;

public class StageResult
{
    public int Id { get; set; }

    public string UserId { get; set; } = default!;

    public string NickName { get; set; } = default!;

    public string Mode { get; set; } = default!; // "Stage3x3", "Stage4x4" 등

    public int Score { get; set; }

    public int ClearTime { get; set; }

    public int MoveCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
