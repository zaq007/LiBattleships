using LiBattleship.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Game.Infrastructure.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid Player1 { get; set; }
        public Guid Player2 { get; set; }
        public Field Map1 { get; set; }
        public Field Map2 { get; set; }
        public bool IsP1Turn { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime LastMoveTime { get; set; }
        public bool IsFinished { get; set; }
        public Guid[] Spectators { get; set; }
    }
}
