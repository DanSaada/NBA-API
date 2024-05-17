using Microsoft.AspNetCore.Mvc;
using NBA_API.Services;
using TP = NBA_API.Models.TopPerformer;

namespace NBA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NBAController : ControllerBase
    {
        private readonly NBAService _nbaService;

        public NBAController(NBAService nbaService)
        {
            _nbaService = nbaService;
        }

        //Retrieves the names of all players, organized by team.
        [HttpGet("GetAllPlayersNames")]
        public ActionResult<Dictionary<string, List<string>>> GetAllPlayersNames()
        {
            try
            {
                var result = _nbaService.GetAllPlayersNames();
                if (result == null || result.Count == 0)
                {
                    return NotFound("No players found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        //Retrieves all action types performed by a specified player.
       [HttpGet("GetAllActionsByPlayerName")]
        public ActionResult<List<string>> GetAllActionsByPlayerName(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Player name is required.");
            }

            var result = _nbaService.GetAllActionTypesByPlayerName(playerName);
            if (result == null || result.Count == 0)
            {
                return NotFound($"No actions found for player: {playerName}");
            }

            return Ok(result);
        }

        //Retrieves the top performers in the game based on points, assists, and rebounds.
        [HttpGet("GetTopPerformers")]
        public ActionResult<List<TP>> GetTopPerformers()
        {
            var result = _nbaService.GetTopPerformers();
            if (result == null || result.Count == 0)
            {
                return NotFound("No top performers found.");
            }

            return Ok(result);
        }
    }
}
