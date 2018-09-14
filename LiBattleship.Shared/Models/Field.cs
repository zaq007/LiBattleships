using LiBattleship.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiBattleship.Shared.Models
{
    public class Field
    {
        enum ShipState
        {
            Unknown = 0,
            Hitted = 16,
            Killed = 32
        }

        /* *
         * CELL BIT STRUCTURE:
         * 
         * 0
         * XXXYZZ
         * 
         * X - SHIP SIZE FIELD (000 if no ship here);
         * Y - HIT STATE;
         * Z - SHIP STATE (SETTED ONLY IF Y = 1);
         *      00 - UNKNOWN OR NO SHIP HERE
         *      01 - HITTED
         *      10 - KILLED
         * */
        
        private readonly int[][] _inner;

        public Field(int[][] field)
        {
            _inner = field;
        }

        private bool GetHitState(int cell)
        {
            return (cell & 8) != 0;
        }

        private ShipState GetShipState(int cell)
        {
            return (ShipState)(cell & 48);
        }

        private int GetShipSize(int cell)
        {
            return cell & 7;
        }

        private void SetHitState(int i, int j, bool state)
        {
            _inner[i][j] = (_inner[i][j] & ~8) | (Convert.ToByte(state) << 3);
        }

        private void SetShipState(int i, int j, ShipState state)
        {
            _inner[i][j] = (_inner[i][j] & ~48) | Convert.ToByte(state);
        }

        private void SetShipSize(int[][] array, int i, int j, int size)
        {
            array[i][j] = (array[i][j] & ~7) | (size & 7);
        }

        public MoveResult MakeMove(int x, int y)
        {
            if (!IsValidMove(x, y)) return MoveResult.IncorrectMove;
            SetHitState(x, y, true);

            if (GetShipSize(_inner[x][y]) != 0)
            {
                SetShipState(x, y, ShipState.Hitted);
                CheckKilledAndSet(x, y);
                if (IsShipCountValid(GetRawData(true))) {
                    return MoveResult.GameFinished;
                }
                return MoveResult.Hit;
            }
            return MoveResult.NoHit;
        }

        private void CheckKilledAndSet(int x, int y)
        {
            var shipCells = new List<(int x, int y)>() { (x, y) };
            var nearestPoints = this.GetNearestPoints(x, y);

            foreach (var (dx, dy) in nearestPoints)
            {
                var i = x;
                var j = y;
                while (true)
                {
                    if (i + dx < _inner.Length && i + dx >= 0
                        && j + dy < _inner.Length && j + dy >= 0 && GetShipSize(_inner[i + dx][j + dy]) > 0)
                    {
                        i = i + dx;
                        j = j + dy;
                    }
                    else break;
                    if (dx == 0 && dy == 0) break;
                    else shipCells.Add((i, j));
                }
            };

            if (shipCells.All(cell => GetShipState(_inner[cell.x][cell.y]) == ShipState.Hitted)) {
                shipCells.ForEach(cell => SetShipState(cell.x, cell.y, ShipState.Killed));
            }
        }

        public int[][] GetRawData(bool masked)
        {
            var result = new int[10][];
            for (int i = 0; i < 10; i++)
            {
                result[i] = new int[10];
                for (int j = 0; j < 10; j++)
                {
                    result[i][j] = _inner[i][j];
                    if (masked && GetShipState(_inner[i][j]) != ShipState.Killed)
                    {
                        SetShipSize(result, i, j, 0);
                    }
                }
            }
            return result;
        }

        private IEnumerable<(int dx, int dy)> GetNearestPoints(int x, int y)
        {
            var result = new List<(int dx, int dy)>();
            if (GetShipSize(_inner[x][y]) > 0) { result.Add((0, 0)); }
            if (x > 0 && GetShipSize(_inner[x - 1][y]) > 0) { result.Add((-1, 0)); }
            if (y < _inner.Length - 1 && GetShipSize(_inner[x][y + 1]) > 0) { result.Add((0, 1)); }
            if (y > 0 && GetShipSize(_inner[x][y - 1]) > 0) { result.Add((0, -1)); }
            if (x < _inner.Length - 1 && GetShipSize(_inner[x + 1][y]) > 0) { result.Add((1, 0)); }
            return result;
        }

        private bool IsValidMove(int i, int j)
        {
            if (GetHitState(_inner[i][j])) return false;
            if (i > 0 && j > 0 && GetShipState(_inner[i - 1][j - 1]) != ShipState.Unknown) return false;
            if (i > 0 && j < _inner.Length - 1 && GetShipState(_inner[i - 1][j + 1]) != ShipState.Unknown) return false;
            if (j > 0 && i < _inner.Length - 1 && GetShipState(_inner[i + 1][j - 1]) != ShipState.Unknown) return false;
            if (j < _inner.Length - 1 && i < _inner.Length - 1 && GetShipState(_inner[i + 1][j + 1]) != ShipState.Unknown) return false;
            //var probableShip = this.getNearestPoints(i, j).Select((point) => _inner[i + point.dx][j + point.dy]).Sum() + 1;
            //if (probableShip > 4) return false;
            return true;
        }


        private bool IsShipCountValid(int[][] field)
        {
            var ships = new Dictionary<int, int>() {
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 }
            };

            for (int i = 0; i < field.Length; i++) {
                for (int j = 0; j < field[i].Length; j++) {
                    var shipSize = GetShipSize(field[i][j]);
                    if (shipSize > 0) ships[shipSize]++;
                }
            }
            foreach (var value in ships) {
                if (value.Value / value.Key != 5 - value.Key) return false;
            }

            return true;
        }
    }
}
