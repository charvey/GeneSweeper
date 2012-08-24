using GeneSweeper.AI;
using GeneSweeper.Game;
using GeneSweeper.Game.Players;
using GeneticAlgorithm;

namespace GeneSweeper
{
    class RuleSetSpecimen:Specimen<RuleSet>
    {
        private RuleSet _ruleSet;

        public RuleSetSpecimen(RuleSet ruleSet)
        {
            _ruleSet = ruleSet;
        }

        private ulong? _fitness;
        public override ulong Fitness()
        {
            if(_fitness.HasValue)
                return _fitness.Value;

            SmartPlayer player = new SmartPlayer(_ruleSet, Board.Difficulty.Small);
            player.Play();

            _fitness =
                (
                    (((ulong) player.Result) << 48) |
                    (((ulong) player.Iterations) << 32) |
                    ~_ruleSet.Count
                );

            return _fitness.Value;
        }

        public override void Mutate()
        {
            throw new System.NotImplementedException();
            _fitness = null;
        }

        public override Specimen<RuleSet> Crossover(Specimen<RuleSet> p1, Specimen<RuleSet> p2)
        {
            throw new System.NotImplementedException();
        }

        public override RuleSet Value()
        {
            return _ruleSet;
        }
    }
}
