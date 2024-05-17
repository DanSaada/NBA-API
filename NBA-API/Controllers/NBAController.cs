using Microsoft.AspNetCore.Mvc;
using NBA_API.Services;
using System;
using System.Collections.Generic;
using ActionModel = NBA_API.Models.Action;

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
    }
}
