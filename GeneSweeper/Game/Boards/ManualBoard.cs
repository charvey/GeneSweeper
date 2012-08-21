using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneSweeper.Game.Boards
{
    public class ManualBoard:Board
    {
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
