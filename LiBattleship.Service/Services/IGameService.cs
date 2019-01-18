using LiBattleship.Game.Models;
using LiBattleship.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LiBattleship.Service.Services
{
    public interface IGameService
    {
        Guid Create(Guid playerId, Field field);
        IEnumerable<Match> GetAvailableMatches();
        GameState GetGameState(Guid id);
        GameState Join(Guid id, Guid guid, Field field);
        GameState MakeMove(Guid id, Guid userGuid, int x, int y);
    }
}
