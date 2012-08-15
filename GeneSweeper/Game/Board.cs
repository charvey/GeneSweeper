using System;
using System.Collections.Generic;
using System.Linq;
using Random = GeneSweeper.Util.Random;

namespace GeneSweeper.Game
{
    public class Board
    {
        #region Structures

        public struct Position
        {
            public byte Row;
            public byte Column;

            public Position(byte row, byte column)
            {
                Row = row;
                Column = column;
            }
        }

        public struct Cell
        {
            public bool Mine;
            public bool Revealed;
            public bool Flagged;
            public byte Neighbors;

            public char ToChar()
            {
                return this.Revealed
                           ? this.Mine
                                 ? 'M'
                                 : this.Neighbors == 0
                                       ? ' '
                                       : ("" + this.Neighbors)[0]
                           : this.Flagged
                                 ? 'F'
                                 : '▒';
            }
        }

        public struct Difficulty
        {
            public byte Height;
            public byte Width;
            public byte Mines;

            public Difficulty(byte height, byte width, byte mines)
            {
                if (width == 0 || height == 0)
                    throw new ArgumentException("Board dimensions must be greater than 0.");
                if (mines >= width*height)
                    throw new ArgumentException("There must be less mines than cells.");

                Height = height;
                Width = width;
                Mines = mines;
            }

            public static readonly Difficulty Small = new Difficulty(8, 8, 8);
            public static readonly Difficulty Beginner = new Difficulty(9, 9, 10);
            public static readonly Difficulty Intermediate = new Difficulty(16, 16, 40);
            public static readonly Difficulty Advanced = new Difficulty(16, 30, 99);
        }

        #endregion

        #region Enums

        public enum State : byte
        {
            Playing,
            Won,
            Lost
        }

        #endregion

        #region Private Fields

        private readonly Cell[,] _board;

        #endregion

        #region Public Fields

        public Cell this[int r, int c]
        {
            get
            {
                if (r < 0 || c < 0 || r >= _board.GetLength(0) || c >= _board.GetLength(1))
                    throw new ArgumentException("Positions must be within the board.");

                return _board[r, c];
            }
        }

        public State CurrentState { get; private set; }
        public Difficulty CurrentDifficulty { get; private set; }

        #endregion

        #region Constructors

        public Board()
            : this(Difficulty.Beginner)
        {
        }

        public Board(byte height = 50, byte width = 50, byte mines = 50)
            : this(new Difficulty(height, width, mines))
        {
        }

        public Board(Difficulty difficulty)
        {
            _board = new Cell[difficulty.Height,difficulty.Width];
            CurrentState = State.Playing;
            CurrentDifficulty = difficulty;

            for(int m=0;m<CurrentDifficulty.Mines;m++)
                AddRandomMine();
        }

        #endregion

        #region Private Helpers

        private IEnumerable<Position> NeighboringPositions(byte row, byte column)
        {
            return NeighboringPositions(new Position(row, column));
        } 

        private IEnumerable<Position> NeighboringPositions(Position position)
        {
            byte rMin = (byte) (position.Row - (position.Row > 0 ? 1 : 0)),
                 rMax = (byte) (position.Row + (position.Row < CurrentDifficulty.Height - 1 ? 1 : 0)),
                 cMin = (byte) (position.Column - (position.Column > 0 ? 1 : 0)),
                 cMax = (byte) (position.Column + (position.Column < CurrentDifficulty.Width - 1 ? 1 : 0));

            for (byte r = rMin; r <= rMax; r++)
                for (byte c = cMin; c <= cMax; c++)
                    if (!(r == position.Row && c == position.Column))
                        yield return new Position(r, c);
        }

        #endregion

        #region Private Methods

        private void AddRandomMine()
        {
            while (true)
            {  
                int c = Random.NextInt(CurrentDifficulty.Width+1);
                int r = Random.NextInt(CurrentDifficulty.Height+1);

                if (!_board[r, c].Mine)
                {
                    _board[r, c].Mine = true;

                    foreach (var nCell in NeighboringPositions((byte)r, (byte)c))
                    {
                        _board[nCell.Row, nCell.Column].Neighbors++;
                    }

                    return;
                }
            }
        }

        #endregion

        #region Public Methods

        public void Flag(int r, int c)
        {
            if(_board[r,c].Revealed)
                throw new ArgumentException("This position has already been revealed.");
            if (_board[r, c].Flagged)
                throw new ArgumentException("This position has already been flagged.");

            _board[r, c].Flagged = true;
        }

        public void Reveal(Position position)
        {
            Reveal(position.Row,position.Column);
        }

        public void Reveal(byte r, byte c)
        {
            if (_board[r, c].Revealed)
                throw new ArgumentException("This position has already been revealed.");
            if (_board[r, c].Flagged)
                throw new ArgumentException("This position has already been flagged.");

            _board[r, c].Revealed = true;

            if(_board[r,c].Mine)
            {
                CurrentState = State.Lost;
            }
            else if(_board[r,c].Neighbors==0)
            {
                foreach (var nCell in NeighboringPositions(r,c))
                {
                    if (!_board[nCell.Row, nCell.Column].Revealed)
                        Reveal(nCell);
                }
            }
        }

        public ushort Score()
        {
            if (CurrentState == State.Lost)
                return 0;

            ushort score = 0;
            foreach (var cell in _board)
            {
                if (cell.Mine && cell.Flagged)
                    score++;
            }

            return score;
        }

        #endregion

        public override string ToString()
        {
            string str = "";

            str += "   " + Enumerable.Range(0, CurrentDifficulty.Width).Select(c => c/10).Aggregate("", (s, c) => s + c) +'\n';
            str += "   " + Enumerable.Range(0, CurrentDifficulty.Width).Select(c => c%10).Aggregate("", (s, c) => s + c) +'\n';
            str += "  ╔" + new string(Enumerable.Repeat('═', CurrentDifficulty.Width).ToArray()) + "╗\n";

            for(byte r=0;r<CurrentDifficulty.Height;r++)
            {
                str += r.ToString("00");
                str += '║';
                for (byte c = 0; c < CurrentDifficulty.Width; c++)
                {
                    Cell cell = _board[r, c];
                    str += cell.ToChar();
                }
                str += '║';
                str += '\n';
            }

            str += "  ╚" + new string(Enumerable.Repeat('═', CurrentDifficulty.Width).ToArray()) + "╝\n";

            return str;
        }
    }
}
