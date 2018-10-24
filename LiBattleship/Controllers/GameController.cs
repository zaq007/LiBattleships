using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LiBattleship.Game;
using LiBattleship.Hubs;
using LiBattleship.Identity;
using LiBattleship.Matchmaking;
using LiBattleship.Service.Services;
using LiBattleship.Shared.Models;
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
        private readonly IGameService gameService;
        private readonly IHubContext<BattleshipsHub> hub;

        public GameController(IGameService gameService, IHubContext<BattleshipsHub> hub)
        {
            this.hub = hub;
            this.gameService = gameService;
        }

        [Route("")]
        [HttpPost]
        public IActionResult Create([FromBody] int[][] field)
        {
            gameService.Create(User.GetUserId(), new Field(field));
            hub.Clients.All.SendAsync("setGameList", gameService.GetAvailableMatches());
            return Ok();
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAvailableMatches()
        {
            return Ok(gameService.GetAvailableMatches());
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetGameState(Guid id)
        {
            var userGuid = User.GetUserId();
            return Ok(gameService.GetGameState(id).ForPlayer(userGuid));
        }

        [Route("{id}/join")]
        [HttpPost]
        public IActionResult JoinGame(Guid id, [FromBody] int[][] field)
        {
            var game = gameService.Join(id, User.GetUserId(), new Field(field));
            if (game == null) return BadRequest();
            hub.Clients.User(game.Player1.ToString()).SendAsync("gameCreated", game.ForPlayer(game.Player1));
            return Ok(game.ForPlayer(User.GetUserId()));
        }

        [Route("{id}/move/{x}/{y}")]
        [HttpPost]
        public IActionResult MakeMove(Guid id, int x, int y)
        {
            var userGuid = User.GetUserId();
            var state = gameService.MakeMove(id, userGuid, x, y);
            if (state != null)
            {
                var otherPlayer = state.IsP1Turn ? state.Player1 : state.Player2;
                hub.Clients.User(otherPlayer.ToString()).SendAsync("setGameState", state.ForPlayer(otherPlayer));
                return Ok(state.ForPlayer(userGuid));
            }
            return Ok(gameService.GetGameState(id).ForPlayer(userGuid));
        }
    }
}