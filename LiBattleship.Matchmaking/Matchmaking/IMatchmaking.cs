using LiBattleship.Shared.Models;
using System;
using System.Collections.Generic;

namespace LiBattleship.Matchmaking
{
    public interface IMatchmaking
    {
        Guid CreateMatch(Guid creator, Field map);
        Match JoinMatch(Guid match, Guid joiner, Field map);
        IEnumerable<Match> GetAvailableMatches();
    }
}
