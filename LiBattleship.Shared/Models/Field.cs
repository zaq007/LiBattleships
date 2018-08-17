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
            Hitted = 8,
            Killed = 16
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
            _inner[i][j] = (_inner[i][j] & ~48) | (Convert.ToByte(state) << 4);
        }

        private void SetShipSize(int[][] array, int i, int j, int size)
        {
            array[i][j] = (array[i][j] & ~7) | (size & 7);
        }

        public bool MakeMove(int x, int y)
        {
            if (!IsValidMove(x, y)) return false;
            SetHitState(x, y, true);

            if (GetShipSize(_inner[x][y]) != 0)
            {
                SetShipState(x, y, ShipState.Hitted);
            }
            return true;
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

        private void setPointWeight(int x, int y, int weight)
        {
            _inner[x][y] = weight;
            if (_inner[x][y] > 0) this.setRegionWeight(x, y, this.getRegionWeight(x, y));
            else
            {
                var nearestPoints = this.getNearestPoints(x, y);
                foreach (var point in nearestPoints)
                {
                    if (point.dx != 0 || point.dy != 0) this.setRegionWeight(x + point.dx, y + point.dy, this.getRegionWeight(x + point.dx, y + point.dy));
                }
            }
        }

        private int getRegionWeight(int x, int y)
        {
            var sum = 0;
            var nearestPoints = this.getNearestPoints(x, y);

            foreach (var point in nearestPoints)
            {
                var i = x;
                var j = y;
                while (true)
                {
                    sum++;
                    if (i + point.dx < _inner.Length && i + point.dx >= 0
                        && j + point.dy < _inner.Length && j + point.dy >= 0 && _inner[i + point.dx][j + point.dy] > 0)
                    {
                        i = i + point.dx;
                        j = j + point.dy;
                    }
                    else break;
                    if (point.dx == 0 && point.dy == 0) break;
                }
            };

            return sum - nearestPoints.Count() + 1;
        }

        private void setRegionWeight(int x, int y, int weight)
        {
            var nearest = this.getNearestPoints(x, y);
            foreach (var point in nearest)
            {
                var i = x;
                var j = y;
                while (true)
                {
                    _inner[i][j] = weight;
                    if (i + point.dx < _inner.Length && i + point.dx >= 0 && j + point.dy < _inner.Length
                        && j + point.dy >= 0 && _inner[i + point.dx][j + point.dy] > 0)
                    {
                        i = i + point.dx;
                        j = j + point.dy;
                    }
                    else break;
                    if (point.dx == 0 && point.dy == 0) break;
                }
            }
        }

        private IEnumerable<(int dx, int dy)> getNearestPoints(int x, int y)
        {
            var result = new List<(int dx, int dy)>();
            if (_inner[x][y] > 0) { result.Add((0, 0)); }
            if (x > 0 && _inner[x - 1][y] > 0) { result.Add((-1, 0)); }
            if (y < _inner.Length - 1 && _inner[x][y + 1] > 0) { result.Add((0, 1)); }
            if (y > 0 && _inner[x][y - 1] > 0) { result.Add((0, -1)); }
            if (x < _inner.Length - 1 && _inner[x + 1][y] > 0) { result.Add((1, 0)); }
            return result;
        }

        private bool IsValidMove(int i, int j)
        {
            if (GetHitState(_inner[i - 1][j - 1])) return false;
            if (i > 0 && j > 0 && GetShipState(_inner[i - 1][j - 1]) != ShipState.Unknown) return false;
            if (i > 0 && j < _inner.Length - 1 && GetShipState(_inner[i - 1][j + 1]) != ShipState.Unknown) return false;
            if (j > 0 && i < _inner.Length - 1 && GetShipState(_inner[i + 1][j - 1]) != ShipState.Unknown) return false;
            if (j < _inner.Length - 1 && i < _inner.Length - 1 && GetShipState(_inner[i + 1][j + 1]) != ShipState.Unknown) return false;
            //var probableShip = this.getNearestPoints(i, j).Select((point) => _inner[i + point.dx][j + point.dy]).Sum() + 1;
            //if (probableShip > 4) return false;
            return true;
        }
    }
}
