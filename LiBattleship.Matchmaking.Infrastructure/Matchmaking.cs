using LiBattleship.Matchmaking.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiBattleship.Matchmaking.Infrastructure
{
    public class Matchmaking : IMatchmaking
    {
        private readonly List<Match> _matches;

        public Matchmaking()
        {
            _matches = new List<Match>();
        }

        public Guid CreateMatch(Guid creator, int[][] map)
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

        public bool JoinMatch(Guid match, Guid joiner, int[][] map)
        {
            throw new NotImplementedException();
        }
    }
}
