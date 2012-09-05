using System;
using System.Collections.Generic;

namespace GeneSweeper.Game
{
    public abstract class Board
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

        public struct Square
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

        public class Difficulty
        {
            public byte Height;
            public byte Width;
            public byte Mines;

            public Difficulty(byte height, byte width, byte mines)
            {
                if (width == 0 || height == 0)
                    throw new ArgumentException("Board dimensions must be greater than 0.");
                if (mines >= width * height)
                    throw new ArgumentException("There must be less mines than cells.");

                Height = height;
                Width = width;
                Mines = mines;
            }

            //public static readonly Difficulty Small = new Difficulty(8, 8, 8);
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

        #region Public Fields

        public abstract Square this[Position p] { get; }

        public State CurrentState { get; protected set; }
        public Difficulty CurrentDifficulty { get; protected set; }

        #endregion

        #region Public Methods

        public abstract void Flag(Position position);

        public abstract ISet<Position> Reveal(Position position);

        public abstract ushort Score();

        #endregion
    }
}
