using LiBattleship.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiBattleship.Game.Models
{
    public class GameState
    {
        public Guid Id { get; set; }
        public Guid Player1 { get; set; }
        public Guid Player2 { get; set; }
        public Field Map1 { get; set; }
        public Field Map2 { get; set; }
        public bool IsP1Turn { get; set; }
        public bool IsFinished { get; set; }
        public DateTime LastMoveTime { get; set; }

        public PlayerGameState ForPlayer(Guid playerId)
        {
            var playerGameState = new PlayerGameState()
            {
                Id = Id,
                IsFinished = IsFinished
            };

            if (!IsFinished)
            {
                playerGameState.MyMap = playerId == Player1 ? Map1.GetRawData(false) : Map2.GetRawData(false);
                playerGameState.EnemyMap = playerId == Player1 ? Map2.GetRawData(true) : Map1.GetRawData(true);
                playerGameState.IsMyTurn = playerId == Player1 ? IsP1Turn : !IsP1Turn;
            }
            else
            {
                playerGameState.MyMap = playerId == Player1 ? Map1.GetRawData(false) : Map2.GetRawData(false);
                playerGameState.EnemyMap = playerId == Player1 ? Map2.GetRawData(false) : Map1.GetRawData(false);
                playerGameState.IsMyTurn = false;
            }

            return playerGameState;
        }
    }
}
