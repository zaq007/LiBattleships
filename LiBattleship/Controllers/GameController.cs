using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LiBattleship.Game;
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
        private readonly IGameServer gameServer;
        private readonly IHubContext<BattleshipsHub> hub;

        public GameController(IMatchmaking matchmaking, IGameServer gameServer, IHubContext<BattleshipsHub> hub)
        {
            this.matchmaking = matchmaking;
            this.hub = hub;
            this.gameServer = gameServer;
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

        [Route("")]
        [HttpGet]
        public IActionResult GetAvailableMatches()
        {
            var userGuid = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            return Ok(matchmaking.GetAvailableMatches());
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetGameState(Guid id)
        {
            var userGuid = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            return Ok(gameServer.GetGameState(id).ForPlayer(userGuid));
        }

        [Route("{id}/join")]
        [HttpPost]
        public IActionResult JoinGame(Guid id, [FromBody] int[][] field)
        {
            var userGuid = Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var match = matchmaking.JoinMatch(id, userGuid, field);
            if (match == null) return BadRequest();
            var game = gameServer.CreateGame(match);
            hub.Clients.User(match.Creator.ToString()).SendAsync("gameCreated", game.ForPlayer(match.Creator));
            return Ok(game.ForPlayer(userGuid));
        }

        [Route("{id}/move/{x}/{y}")]
        [HttpPost]
        public IActionResult MakeMove(Guid id, int x, int y)
        {
            var userGuid = Guid.Parse(User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var state = gameServer.MakeMove(id, userGuid, x, y);
            if (state != null)
            {
                var otherPlayer = state.IsP1Turn ? state.Player1 : state.Player2;
                hub.Clients.User(otherPlayer.ToString()).SendAsync("setGameState", state.ForPlayer(otherPlayer));
                return Ok(state.ForPlayer(userGuid));
            }
            return Ok(gameServer.GetGameState(id).ForPlayer(userGuid));
        }
    }
}