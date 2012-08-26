using System;
using System.Collections.Generic;
using System.Linq;
using Random = GeneticAlgorithm.Random;

namespace GeneSweeper.Game.Boards
{
    public class AutoBoard:Board
    {

        #region Private Fields

        private readonly Square[,] _board;

        #endregion

        #region Constructors

        public AutoBoard()
            : this(Difficulty.Beginner)
        {
        }

        public AutoBoard(Difficulty difficulty)
        {
            _board = new Square[difficulty.Height,difficulty.Width];
            CurrentState = State.Playing;
            CurrentDifficulty = difficulty;

            AddRandomMines(CurrentDifficulty.Mines);
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

        private void AddRandomMines(int count =1)
        {
            while (count>0)
            {  
                byte[] b = Random.NextBytes(2);
                byte c = (byte) (b[0]%CurrentDifficulty.Width),
                     r = (byte) (b[1]%CurrentDifficulty.Height);

                if (!_board[r, c].Mine)
                {
                    _board[r, c].Mine = true;

                    foreach (var nCell in NeighboringPositions(r, c))
                    {
                        _board[nCell.Row, nCell.Column].Neighbors++;
                    }

                    count--;
                }
            }
        }

        private ISet<Position> Reveal(byte r, byte c)
        {
            if (_board[r, c].Revealed)
                throw new ArgumentException("This position has already been revealed.");
            if (_board[r, c].Flagged)
                throw new ArgumentException("This position has already been flagged.");

            ISet<Position> revealed = new HashSet<Position> { new Position(r, c) };
            
            _board[r, c].Revealed = true;

            if (_board[r, c].Mine)
            {
                CurrentState = State.Lost;
            }
            else if (_board[r, c].Neighbors == 0)
            {
                foreach (var nCell in NeighboringPositions(r, c))
                {
                    if (!_board[nCell.Row, nCell.Column].Revealed)
                    {
                        revealed.UnionWith(Reveal(nCell.Row, nCell.Column));
                    }
                }
            }

            return revealed;
        }

        #endregion

        #region Public Methods

        public override Square this[Position p]
        {
            get { return _board[p.Row, p.Column]; }
        }

        public override void Flag(Position position)
        {
            if (_board[position.Row, position.Column].Revealed)
                throw new ArgumentException("This position has already been revealed.");
            if (_board[position.Row, position.Column].Flagged)
                throw new ArgumentException("This position has already been flagged.");

            _board[position.Row, position.Column].Flagged = true;
        }

        public override ISet<Position> Reveal(Position position)
        {
            return Reveal(position.Row,position.Column);
        }

        public override ushort Score()
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
                    Square square = _board[r, c];
                    str += square.ToChar();
                }
                str += '║';
                str += '\n';
            }

            str += "  ╚" + new string(Enumerable.Repeat('═', CurrentDifficulty.Width).ToArray()) + "╝\n";

            return str;
        }
    }
}
