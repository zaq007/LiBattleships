using LiBattleship.Game.Models;
using LiBattleship.Shared.Models;
using System;

namespace LiBattleship.Game
{
    public interface IGameServer
    {
        GameState CreateGame(Match match);
        GameState GetGameState(Guid gameId);
        GameState MakeMove(Guid gameId, Guid playerId, int x, int y);
    }
}
