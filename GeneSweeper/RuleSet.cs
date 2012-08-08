using System.Collections.Generic;
using System.Linq;

namespace GeneSweeper
{
    public class RuleSet
    {
        private Dictionary<NeighborhoodState,CellState> rules;

        public RuleSet()
        {
            rules = new Dictionary<NeighborhoodState, CellState>();
        }

        public void Add(Rule rule)
        {
            rules[rule.Pattern] = rule.Result;
        }

        public CellState? Get(NeighborhoodState pattern)
        {
            if (rules.ContainsKey(pattern))
                return rules[pattern];

            return null;
        }

        public uint Count { get { return (uint) rules.Count; } }

        public static RuleSet GenerateRandom(int n = 10000000)
        {
            RuleSet ruleSet = new RuleSet();

            for(int i=0;i<n;i++)
                ruleSet.Add(Random.NextRule());

            return ruleSet;
        }
    }
}
