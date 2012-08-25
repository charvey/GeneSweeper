﻿using System.Collections.Generic;
using GeneSweeper.Game;
using GeneSweeper.Util;
using System;

namespace GeneSweeper.AI
{
    public class RuleSet:ICloneable
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

        public void Remove(NeighborhoodState pattern)
        {
            if (rules.ContainsKey(pattern))
                rules.Remove(pattern);
        }

        public uint Count { get { return (uint) rules.Count; } }

        public object Clone()
        {
            var clone = new RuleSet();

            foreach (var x in rules)
            {
                clone.Add(new Rule(x.Key, x.Value));
            }

            return clone;
        }
    }
}
