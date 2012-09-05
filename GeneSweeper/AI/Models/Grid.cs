using System.Diagnostics;
using GeneSweeper.Game;
using GeneSweeper.Util;
using GeneticAlgorithm;
using System.Linq;

namespace GeneSweeper.AI.Models
{
    public class Grid
    {
        private CellState[,] _grid;
        private readonly int _rows;
        private readonly int _cols;

        #region Constructor

        public Grid(byte rows, byte cols)
        {
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
                    _grid[r, c] = CellState.Initial;
                }
            }
        }

        #endregion

        #region Accessors

        public CellState GetCellState(byte row, byte col)
        {
            return _grid[row, col];
        }

        public void SetCellState(byte row, byte col,CellState value)
        {
            _grid[row, col] = value;
        }

        public NeighborhoodState GetNeighborhoodState(byte row, byte col)
        {
            int r = row, c = col;

            return new NeighborhoodState(_grid[r - 1, c - 1].Value,
                                         _grid[r - 1, c].Value,
                                         _grid[r - 1, c + 1].Value,
                                         _grid[r, c - 1].Value,
                                         _grid[r, c].Value,
                                         _grid[r, c + 1].Value,
                                         _grid[r + 1, c - 1].Value,
                                         _grid[r + 1, c].Value,
                                         _grid[r + 1, c + 1].Value);
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
            //string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            string chars = "012345678█!?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            //string chars = "012345678█!?▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒";

            str += "    " + Enumerable.Range(0, _cols).Select(c => c / 10).Aggregate("", (s, c) => s + c) + '\n';
            str += "    " + Enumerable.Range(0, _cols).Select(c => c % 10).Aggregate("", (s, c) => s + c) + '\n';

            for (int r = 0; r <= _rows + 1; r++)
            {
                if (r > 0 && r <= _rows)
                    str += (r - 1).ToString("00");
                else
                    str += "  ";

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
