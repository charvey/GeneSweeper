using System.Collections.Generic;

namespace GeneSweeper
{
    public class RuleSet
    {
        private Dictionary<ulong,byte> rules;

        public RuleSet()
        {
            rules=new Dictionary<ulong, byte>();
        }

        public void Add(Rule rule)
        {
            rules[rule.NeighborState] = rule.ResultState;
        }
    }
}
