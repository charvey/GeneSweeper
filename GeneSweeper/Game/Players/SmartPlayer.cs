using GeneSweeper.AI;
using GeneSweeper.Game.Boards;

namespace GeneSweeper.Game.Players
{
    class SmartPlayer:Player
    {
        private Grid _grid;
        private RuleSet _ruleSet;
        
        public SmartPlayer(RuleSet ruleSet, Board board)
            : base(board)
        {
            _ruleSet = ruleSet;
            _grid = new Grid(board.CurrentDifficulty.Height, board.CurrentDifficulty.Width);
        }

        public override void Play()
        {
            int countSinceReveal = 0;
            while (Board.CurrentState==Board.State.Playing)
            {
                bool halt = _grid.Apply(_ruleSet);

                if (halt || countSinceReveal > 1000000)
                    return;

                for(byte r=1;r<=Board.CurrentDifficulty.Height;r++)
                {
                    for(byte c=1;c<=Board.CurrentDifficulty.Width;c++)
                    {
                        if(_grid.GetCellState(r,c).Value==CellState.Reveal.Value)
                        {
                            countSinceReveal = 0;

                            var updates = Board.Reveal(new Board.Position((byte) (r - 1), (byte) (c - 1)));

                            foreach (var update in updates)
                            {
                                _grid.SetCellState(update.Row, update.Column, new CellState(Board[update].Neighbors));
                            }
                        }
                    }
                }

                countSinceReveal++;
            }
        }

        public override string ToString()
        {
            string[] brd = Board.ToString().Split('\n');
            string[] grd = _grid.ToString().Split('\n');

            string str = "";
            for (int i = 0; i < brd.Length;i++ )
            {
                str += brd[i] + grd[i] + '\n';
            }

            return str;
        }
    }
}
