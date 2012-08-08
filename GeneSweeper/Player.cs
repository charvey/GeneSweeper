using System;

namespace GeneSweeper
{
    public abstract class Player
    {
        protected Board Board;

        protected Player(Board.Difficulty difficulty)
            : this(new Board(difficulty))
        {
        }

        protected Player(Board board)
        {
            Board = board;
        }

        public abstract void Play();
        public ushort Result
        {
            get { return Board.Score(); }
        }
    }
}
