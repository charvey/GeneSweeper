using System.Collections.Generic;
using GeneSweeper.Game;
using GeneSweeper.Util;
using System;
using GeneticAlgorithm;

namespace GeneSweeper.AI.Models
{
    public class RuleSet
    {
        public Dictionary<NeighborhoodState, CellState> Rules { get; private set; }

        public RuleSet()
        {
            Rules = new Dictionary<NeighborhoodState, CellState>();
        }

        public RuleSet(Dictionary<NeighborhoodState, CellState> rules)
        {
            Rules = rules;
        }

        public void Add(Rule rule)
        {
            Rules[rule.Pattern] = rule.Result;
        }

        public CellState? Get(NeighborhoodState pattern)
        {
            CellState result;

            if (Rules.TryGetValue(pattern, out result))
                return result;

            return null;
        }

        public void Remove(NeighborhoodState pattern)
        {
            if (Rules.ContainsKey(pattern))
                Rules.Remove(pattern);
        }

        
    }
}
