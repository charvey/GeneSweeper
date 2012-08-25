using System;
using System.Collections.Generic;

namespace GeneSweeper.Game.Boards
{
    public class ManualBoard:Board
    {
        public override Square this[Position p]
        {
            get { throw new NotImplementedException(); }
        }

        public override void Flag(Position position)
        {
            throw new NotImplementedException();
        }

        public override ISet<Position> Reveal(Position position)
        {
            throw new NotImplementedException();
        }

        public override ushort Score()
        {
            throw new NotImplementedException();
        }
    }
}
