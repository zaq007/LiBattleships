using LiBattleship.Game;
using LiBattleship.Game.Models;
using LiBattleship.Matchmaking;
using LiBattleship.Service.Services;
using LiBattleship.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiBattleship.Service.Infrastructure.Services
{
    public class GameService : IGameService
    {
        readonly IMatchmaking matchmaking;
        readonly IGameServer gameServer;

        public GameService(IMatchmaking matchmaking, IGameServer gameServer)
        {
            this.matchmaking = matchmaking;
            this.gameServer = gameServer;
        }

        public Guid Create(Guid playerId, Field field)
        {
            return matchmaking.CreateMatch(playerId, field);
        }

        public IEnumerable<Match> GetAvailableMatches()
        {
            return matchmaking.GetAvailableMatches();
        }

        public GameState GetGameState(Guid id)
        {
            return gameServer.GetGameState(id);
        }

        public GameState Join(Guid matchId, Guid userId, Field field)
        {
            var match = matchmaking.JoinMatch(matchId, userId, field);
            if (match != null)
            {
                return gameServer.CreateGame(match);
            }
            return null;
        }

        public GameState MakeMove(Guid id, Guid userGuid, int x, int y)
        {
            return gameServer.MakeMove(id, userGuid, x, y);
        }
    }
}
