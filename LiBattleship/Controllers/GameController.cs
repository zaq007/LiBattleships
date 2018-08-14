using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LiBattleship.Hubs;
using LiBattleship.Matchmaking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LiBattleship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IMatchmaking matchmaking;
        private readonly IHubContext<BattleshipsHub> hub;

        public GameController(IMatchmaking matchmaking, IHubContext<BattleshipsHub> hub)
        {
            this.matchmaking = matchmaking;
            this.hub = hub;
        }

        [Route("")]
        [HttpPost]
        public IActionResult Create([FromBody] int[][] field)
        {
            var userGuid = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var matchGuid = matchmaking.CreateMatch(userGuid, field);
            hub.Clients.All.SendAsync("setGameList", matchmaking.GetAvailableMatches());
            return Ok();
        }
    }
}