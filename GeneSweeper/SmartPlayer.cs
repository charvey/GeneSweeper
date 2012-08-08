using System;

namespace GeneSweeper
{
    class SmartPlayer:Player
    {
        private Grid _grid;
        private RuleSet _ruleSet;
        public ushort Iterations { get; private set; }

        public SmartPlayer(RuleSet ruleSet, Board.Difficulty difficulty)
            : this(ruleSet, new Board(difficulty))
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
    }
}
