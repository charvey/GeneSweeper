using GeneSweeper.AI;
using GeneSweeper.Game.Boards;

namespace GeneSweeper.Game.Players
{
    class SmartPlayer:Player
    {
        private Grid _grid;
        private RuleSet _ruleSet;
        public ushort Iterations { get; private set; }

        public SmartPlayer(RuleSet ruleSet, Board.Difficulty difficulty)
            : this(ruleSet, new AutoBoard(difficulty))
        {
        }

        public SmartPlayer(RuleSet ruleSet, Board board)
            : base(board)
        {
            _ruleSet = ruleSet;
            Iterations = 0;
        }

        public override void Play()
        {
            while (true)
            {
                _grid.Apply(_ruleSet);
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
