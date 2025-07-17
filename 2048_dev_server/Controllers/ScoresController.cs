using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2048_dev_server.Data;
using _2048_dev_server.Models;

namespace _2048_dev_server.Controllers;

[ApiController]
[Route("api/scores")]
public class ScoresController : ControllerBase
{
    private readonly GameDbContext _db;

    public ScoresController(GameDbContext db)
    {
        _db = db;
    }

    // GET: api/scores/ranking
    [HttpGet("ranking")]
    public async Task<ActionResult<IEnumerable<object>>> GetRanking()
    {
        var ranking = await _db.Scores
            .Include(s => s.User)
            .OrderByDescending(s => s.Value)
            .Take(10)
            .Select(s => new
            {
                s.Id,
                s.Value,
                s.CreatedAt,
                s.UserId,
                s.User!.NickName
            })
            .ToListAsync();

        return Ok(ranking);
    }

    // POST: api/scores
    [HttpPost]
    public async Task<ActionResult<Score>> SubmitScore(Score score)
    {
        _db.Scores.Add(score);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(SubmitScore), new { id = score.Id }, score);
    }
}