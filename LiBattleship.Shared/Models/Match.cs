using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Shared.Models
{
    public class Match
    {
        public Guid Id { get; set; }
        public Guid Creator { get; set; }
        public Guid Joiner { get; set; }
        public int[][] CreatorMap { get; set; }
        public int[][] JoinerMap { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
