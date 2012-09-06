using GeneSweeper.AI;
using GeneSweeper.AI.Models;
using GeneSweeper.Game.Boards;

namespace GeneSweeper.Game.Players
{
    class SmartPlayer:Player
    {
        #region Private Fields

        private Grid _grid;
        private RuleSet _ruleSet;
        private int _step;

        #endregion

        #region Public Fields

        public int StepsSinceReveal { get; private set; }
        public ushort Reveals { get; private set; }

        #endregion

        #region Constructor

        public SmartPlayer(RuleSet ruleSet, Board board)
            : base(board)
        {
            _ruleSet = ruleSet;
            _grid = new Grid(board.CurrentDifficulty.Height, board.CurrentDifficulty.Width);
            _step = 0;
            
            StepsSinceReveal = 0;
            Reveals = 0;
        }

        #endregion

        #region Public Accessors

        public Board.State GetCurrentState()
        {
            return Board.CurrentState;
        }

        #endregion

        #region Public Methods

        public override void Play()
        {
            while (Board.CurrentState==Board.State.Playing)
            {
                bool halt = Step();

                if (halt || StepsSinceReveal > 1000000)
                    return;
            }
        }

        public bool Step()
        {
            if (_step == 0)
            {
                _grid.SetCellState(
                    (byte)((Board.CurrentDifficulty.Height + 1) / 2),
                    (byte)((Board.CurrentDifficulty.Width + 1) / 2),
                    CellState.Reveal);
            }
            else
            {
                if(_grid.Apply(_ruleSet))
                    return true;
            }
            StepsSinceReveal++;
            _step++;

            for (byte r = 1; r <= Board.CurrentDifficulty.Height; r++)
            {
                for (byte c = 1; c <= Board.CurrentDifficulty.Width; c++)
                {
                    if (_grid.GetCellState(r, c).Value == CellState.Reveal.Value)
                    {
                        StepsSinceReveal = 0;
                        Reveals++;

                        var updates = Board.Reveal(new Board.Position((byte)(r - 1), (byte)(c - 1)));

                        foreach (var update in updates)
                        {
                            _grid.SetCellState(
                                (byte)(update.Row + 1),
                                (byte)(update.Column + 1),
                                new CellState(Board[update].Neighbors));
                        }
                    }
                }
            }

            return false;
        }

        #endregion

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
