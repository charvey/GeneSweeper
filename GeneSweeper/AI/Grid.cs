using System.Diagnostics;
using GeneSweeper.Game;
using GeneSweeper.Util;

namespace GeneSweeper.AI
{
    public class Grid
    {
        private SquareState[,] values;
        private readonly int _rows;
        private readonly int _cols;

        public Grid(byte rows, byte cols)
        {
            Debug.Assert(rows > 0 && cols > 0);

            _rows = rows;
            _cols = cols;

            values = new SquareState[_rows + 2,_cols + 2];

            for (int i = 0; i <= _rows + 1; i++)
            {
                values[i, 0] = SquareState.Edge;
                values[i, _cols + 1] = SquareState.Edge;
            }
            for (int i = 0; i <= _cols + 1; i++)
            {
                values[0, i] = SquareState.Edge;
                values[_rows + 1, i] = SquareState.Edge;
            }

            for (int r = 1; r <= _rows ; r++)
            {
                for (int c = 1; c <= _cols; c++)
                {
                    values[r, c] = new SquareState((byte) Random.Next(0, 9));
                }
            }
        }

        #region Accessors

        public SquareState GetCellState(byte row, byte col)
        {
            Debug.Assert(row > 0 && col > 0);

            return values[row, col];
        }

        public NeighborhoodState GetNeighborhoodState(byte row, byte col)
        {
            Debug.Assert(row > 0 && col > 0);

            ulong value = 0;

            for (int r = row - 1; r <= row + 1; r++)
                for (int c = col - 1; c <= col + 1; c++)
                    value = (value << 6) | ((ulong)values[r, c].Value);

            return new NeighborhoodState(value);
        }

        #endregion

        #region Rule Methods

        public bool Apply(RuleSet ruleSet)
        {
            SquareState[,] newValues = new SquareState[_rows+2,_cols+2];
            bool halt = true;

            for (int i = 0; i <= _rows+1; i++)
            {
                newValues[i, 0] = values[i, 0];
                newValues[i, _cols + 1] = values[i, _cols + 1];
            }
            for (int i = 0; i <= _cols+1; i++)
            {
                newValues[0, i] = values[0, i];
                newValues[_rows+ 1, i] = values[_rows + 1, i];
            }

            for (byte r = 1; r <= _rows; r++)
            {
                for (byte c = 1; c <= _cols; c++)
                {
                    SquareState? result = ruleSet.Get(GetNeighborhoodState(r, c));

                    if(result.HasValue)
                    {
                        halt = false;
                        newValues[r, c] = result.Value;
                    }
                    else
                    {
                        newValues[r, c] = values[r, c];
                    }
                }
            }

            values = newValues;

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
                    str += chars[values[r, c].Value];
                }
                str += '\n';
            }

            return str;
        }
    }
}
