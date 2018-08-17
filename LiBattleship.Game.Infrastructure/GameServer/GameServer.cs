using System;
using System.Collections.Generic;
using System.Linq;
using LiBattleship.Game.Models;
using LiBattleship.Shared.Models;

namespace LiBattleship.Game.Infrastructure
{
    public class GameServer : IGameServer
    {
        private readonly List<Models.Game> _games;

        public GameServer()
        {
            _games = new List<Models.Game>();
        }

        public GameState CreateGame(Match match)
        {
            var game = new Models.Game()
            {
                Id = match.Id,
                Map1 = new Field(match.CreatorMap),
                Map2 = new Field(match.JoinerMap),
                Player1 = match.Creator,
                Player2 = match.Joiner,
                IsP1Turn = true,
                LastMoveTime = DateTime.Now,
                StartTime = DateTime.Now
            };
            _games.Add(game);
            return BuildGameState(game);
        }

        public GameState GetGameState(Guid gameId)
        {
            return BuildGameState(_games.FirstOrDefault(g => g.Id == gameId));
        }

        public GameState MakeMove(Guid gameId, Guid playerId, int x, int y)
        {
            var game = _games.FirstOrDefault(g => g.Id == gameId);

            if (game != null && ((game.Player1 == playerId && game.IsP1Turn) || (game.Player2 == playerId && !game.IsP1Turn)))
            {
                bool result = false;
                if (game.Player1 == playerId) result = game.Map2.MakeMove(x, y);
                if (game.Player2 == playerId) result = game.Map1.MakeMove(x, y);
                if (result)
                {
                    game.IsP1Turn = !game.IsP1Turn;
                    game.LastMoveTime = DateTime.Now;
                }
            }
            return BuildGameState(game);
        }

        private GameState BuildGameState(Models.Game game)
        {
            if (game == null) return null;
            return new GameState()
            {
                Id = game.Id,
                IsP1Turn = game.IsP1Turn,
                Map1 = game.Map1,
                Map2 = game.Map2,
                Player1 = game.Player1,
                Player2 = game.Player2
            };
        }
    }
}
