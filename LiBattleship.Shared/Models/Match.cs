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
        public Field CreatorMap { get; set; }
        public Field JoinerMap { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
