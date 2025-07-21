using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2048_dev_server.Models;
using _2048_dev_server.Data;

namespace _2048_dev_server.Controllers
{
    [ApiController]
    [Route("api/ranking")]
    public class RankingController : ControllerBase
    {
        private readonly GameDbContext _db;

        public RankingController(GameDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetRanking([FromQuery] string mode, [FromQuery] string type = "score")
        {
            var query = _db.StageResults.Where(r => r.Mode == mode);

            query = type switch
            {
                "clearTime" => query.OrderBy(r => r.ClearTime),
                "moveCount" => query.OrderBy(r => r.MoveCount),
                _ => query.OrderByDescending(r => r.Score)
            };

            var ranking = await query
                .Take(20)
                .Select(r => new {
                    r.NickName,
                    r.Score,
                    r.ClearTime,
                    r.MoveCount
                })
                .ToListAsync();

            return Ok(new {
                ok = true,
                data = new { res = ranking },
                error = (object?)null
            });
        }
    }
}
