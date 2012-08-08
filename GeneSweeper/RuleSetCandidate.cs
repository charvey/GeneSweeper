using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneSweeper
{
    class RuleSetCandidate:ICandidate
    {
        private RuleSet _ruleSet;

        public RuleSetCandidate(RuleSet ruleSet)
        {
            _ruleSet = ruleSet;
        }

        private ulong? _fitness;
        public ulong Fitness()
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
    }
}
