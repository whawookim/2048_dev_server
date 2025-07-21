using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2048_dev_server.Models;
using _2048_dev_server.Data;

namespace _2048_dev_server.Controllers
{
    [ApiController]
    [Route("api/stage")]
    public class StageController : ControllerBase
    {
        private readonly GameDbContext _db;

        public StageController(GameDbContext db)
        {
            _db = db;
        }

        [HttpPost("end")]
        public async Task<IActionResult> EndStage([FromBody] EndStageRequest request)
        {
            var result = new StageResult
            {
                UserId = request.UserId,
                NickName = request.NickName,
                Mode = request.Mode,
                Score = request.Score,
                ClearTime = request.ClearTime,
                MoveCount = request.MoveCount
            };

            _db.StageResults.Add(result);
            await _db.SaveChangesAsync();

            return Ok(new {
                ok = true,
                data = new { },
                error = (object?)null
            });
        }
    }
}