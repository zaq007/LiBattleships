using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Shared.Models
{
    public class PlayerGameState
    {
        public Guid Id { get; set; }
        public int[][] MyMap { get; set; }
        public int[][] EnemyMap { get; set; }
        public bool IsMyTurn { get; set; }
        public bool IsFinished { get; set; }
    }
}
