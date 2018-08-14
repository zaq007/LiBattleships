using LiBattleship.Matchmaking.Models;
using System;
using System.Collections.Generic;

namespace LiBattleship.Matchmaking
{
    public interface IMatchmaking
    {
        Guid CreateMatch(Guid creator, int[][] map);
        bool JoinMatch(Guid match, Guid joiner, int[][] map);
        IEnumerable<Match> GetAvailableMatches();
    }
}
