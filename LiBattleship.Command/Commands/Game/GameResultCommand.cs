using LiBattleship.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Command.Commands.Game
{
    public class GameResultCommand: ICommand
    {
        public Guid GameId { get; set; }

        public Guid Player1 { get; set; }

        public Guid Player2 { get; set; }

        public Guid Winner { get; set; }

        public Field Player1Field { get; set; }

        public Field Player2Field { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
