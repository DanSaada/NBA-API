using Microsoft.AspNetCore.Mvc;
using NBA_API.Services;
using System;
using System.Collections.Generic;

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
    }
}
