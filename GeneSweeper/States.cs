using System.Diagnostics;

namespace GeneSweeper
{
    public struct CellState
    {
        public readonly byte Value;

        public CellState(byte value)
        {
            Value = (byte) (value & 63);
        }
    }

    public struct NeighborhoodState
    {
        public readonly ulong Value;

        public NeighborhoodState(ulong value)
        {
            Value = value >> (2 + 6);
        }
    }

    public struct GridState
    {
        private readonly CellState[,] values;

        public GridState(byte rows, byte cols)
        {
            Debug.Assert(rows > 0 && cols > 0);

            values = new CellState[rows + 2,cols + 2];
        }

        public CellState GetCellState(byte row, byte col)
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
                    value = (value << 6) | values[r, c].Value;

            return new NeighborhoodState(value);
        }
    }
}
