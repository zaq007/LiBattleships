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

        public GameController(IGameService gameService, IHubContext<BattleshipsHub> hub)
        {
            this.gameService = gameService;
        }

        [Route("")]
        [HttpPost]
        public IActionResult Create([FromBody] int[][] field)
        {
            gameService.Create(User.GetUserId(), new Field(field));
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
                return Ok(state.ForPlayer(userGuid));
            }
            return Ok(gameService.GetGameState(id).ForPlayer(userGuid));
        }
    }
}