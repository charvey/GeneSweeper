﻿using System.Collections.Generic;
using GeneSweeper.Game;
using GeneSweeper.Util;

namespace GeneSweeper.AI
{
    public class RuleSet
    {
        private Dictionary<NeighborhoodState,SquareState> rules;

        public RuleSet()
        {
            rules = new Dictionary<NeighborhoodState, SquareState>();
        }

        public void Add(Rule rule)
        {
            rules[rule.Pattern] = rule.Result;
        }

        public SquareState? Get(NeighborhoodState pattern)
        {
            if (rules.ContainsKey(pattern))
                return rules[pattern];

            return null;
        }

        public void Remove(NeighborhoodState pattern)
        {
            if (rules.ContainsKey(pattern))
                rules.Remove(pattern);
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