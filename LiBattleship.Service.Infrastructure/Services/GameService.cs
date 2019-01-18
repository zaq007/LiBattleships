using LiBattleship.Game;
using LiBattleship.Game.Models;
using LiBattleship.Matchmaking;
using LiBattleship.Service.Services;
using LiBattleship.Shared.Models;
using LiBattleship.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiBattleship.Service.Infrastructure.Services
{
    public class GameService : IGameService
    {
        readonly IMatchmaking matchmaking;
        readonly IGameServer gameServer;
        readonly INotificationService notificationService;

        public GameService(IMatchmaking matchmaking, IGameServer gameServer, INotificationService notificationService)
        {
            this.matchmaking = matchmaking;
            this.gameServer = gameServer;
            this.notificationService = notificationService;
        }

        public Guid Create(Guid playerId, Field field)
        {
            var guid = matchmaking.CreateMatch(playerId, field);

            notificationService.SetGameList(GetAvailableMatches());

            return guid;
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
                var gameState = gameServer.CreateGame(match);

                notificationService.GameCreated(gameState.Player1, gameState.ForPlayer(gameState.Player1));

                return gameState;
            }
            return null;
        }

        public GameState MakeMove(Guid id, Guid userGuid, int x, int y)
        {
            var state = gameServer.MakeMove(id, userGuid, x, y);

            var otherPlayer = userGuid == state.Player1 ? state.Player2 : state.Player1;
            notificationService.SetGameState(otherPlayer, state.ForPlayer(otherPlayer));

            return state;
        }
    }
}
