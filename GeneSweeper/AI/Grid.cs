using System.Diagnostics;
using GeneSweeper.Game;
using GeneSweeper.Util;
using GeneticAlgorithm;

namespace GeneSweeper.AI
{
    public class Grid
    {
        private CellState[,] _grid;
        private readonly int _rows;
        private readonly int _cols;

        public Grid(byte rows, byte cols)
        {
            Debug.Assert(rows > 0 && cols > 0);

            _rows = rows;
            _cols = cols;

            _grid = new CellState[_rows + 2,_cols + 2];

            for (int i = 0; i <= _rows + 1; i++)
            {
                _grid[i, 0] = CellState.Edge;
                _grid[i, _cols + 1] = CellState.Edge;
            }
            for (int i = 0; i <= _cols + 1; i++)
            {
                _grid[0, i] = CellState.Edge;
                _grid[_rows + 1, i] = CellState.Edge;
            }

            for (int r = 1; r <= _rows ; r++)
            {
                for (int c = 1; c <= _cols; c++)
                {
                    _grid[r, c] = new CellState((byte) Random.NextInt(0, 9));
                }
            }
        }

        #region Accessors

        public CellState GetCellState(byte row, byte col)
        {
            Debug.Assert(row > 0 && col > 0);

            return _grid[row, col];
        }

        public NeighborhoodState GetNeighborhoodState(byte row, byte col)
        {
            Debug.Assert(row > 0 && col > 0);

            ulong value = 0;

            for (int r = row - 1; r <= row + 1; r++)
                for (int c = col - 1; c <= col + 1; c++)
                    value = (value << 6) | ((ulong)_grid[r, c].Value);

            return new NeighborhoodState(value);
        }

        #endregion

        #region Rule Methods

        public bool Apply(RuleSet ruleSet)
        {
            CellState[,] newValues = new CellState[_rows+2,_cols+2];
            bool halt = true;

            for (int i = 0; i <= _rows+1; i++)
            {
                newValues[i, 0] = _grid[i, 0];
                newValues[i, _cols + 1] = _grid[i, _cols + 1];
            }
            for (int i = 0; i <= _cols+1; i++)
            {
                newValues[0, i] = _grid[0, i];
                newValues[_rows+ 1, i] = _grid[_rows + 1, i];
            }

            for (byte r = 1; r <= _rows; r++)
            {
                for (byte c = 1; c <= _cols; c++)
                {
                    CellState? result = ruleSet.Get(GetNeighborhoodState(r, c));

                    if(result.HasValue)
                    {
                        halt = false;
                        newValues[r, c] = result.Value;
                    }
                    else
                    {
                        newValues[r, c] = _grid[r, c];
                    }
                }
            }

            _grid = newValues;

            return halt;
        }

        #endregion

        public override string ToString()
        {
            string str = "";
            //string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";
            //string chars = "012345678█M?▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒";
            string chars = "012345678█Mabcde";

            for (int r = 0; r <= _rows + 1; r++)
            {
                for (int c = 0; c <= _cols + 1; c++)
                {
                    str += chars[_grid[r, c].Value];
                }
                str += '\n';
            }

            return str;
        }
    }
}
