using LiBattleship.Shared.Models;
using System;
using System.Collections.Generic;

namespace LiBattleship.Matchmaking
{
    public interface IMatchmaking
    {
        Guid CreateMatch(Guid creator, int[][] map);
        Match JoinMatch(Guid match, Guid joiner, int[][] map);
        IEnumerable<Match> GetAvailableMatches();
    }
}
