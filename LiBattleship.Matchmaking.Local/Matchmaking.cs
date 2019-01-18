using LiBattleship.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiBattleship.Matchmaking.Local
{
    public class Matchmaking : IMatchmaking
    {
        private readonly List<Match> _matches;

        public Matchmaking()
        {
            _matches = new List<Match>();
        }

        public Guid CreateMatch(Guid creator, Field map)
        {
            var id = Guid.NewGuid();
            _matches.Add(new Match()
            {
                Id = id,
                Creator = creator,
                CreatorMap = map,
                CreationTime = DateTime.Now
            });
            return id;
        }

        public IEnumerable<Match> GetAvailableMatches()
        {
            return _matches.Where(x => x.JoinerMap == null);
        }

        public Match JoinMatch(Guid matchId, Guid joiner, Field map)
        {
            var match = _matches.SingleOrDefault(x => x.Id == matchId);
            if (match.Creator == joiner) return null;
            match.Joiner = joiner;
            match.JoinerMap = map;
            _matches.Remove(match);
            return match;
        }
    }
}
