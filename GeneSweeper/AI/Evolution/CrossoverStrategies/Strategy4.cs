using GeneSweeper.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Random = GeneticAlgorithm.Random;

namespace GeneSweeper.AI.Evolution.CrossoverStrategies
{
    public class Strategy4 :ICrossoverStrategy
    {
        public RuleSetSpecimen Crossover(RuleSetSpecimen p1, RuleSetSpecimen p2)
        {
            int size = (p1.RuleSet.Rules.Count + p2.RuleSet.Rules.Count) / 2;

            int p1Segment, p2Segment, offset, remaining;

            Dictionary<NeighborhoodState,CellState> rules = new Dictionary<NeighborhoodState,CellState>();

            do
            {
                remaining = size - rules.Count;

                p1Segment = (int)(Random.NextDouble() * remaining);

                if (Random.NextBool())
                    p1Segment = remaining - p1Segment;

                p2Segment = remaining - p1Segment;                

                p1Segment = Math.Min(p1Segment, p1.RuleSet.Rules.Count);
                p2Segment = Math.Min(p2Segment, p2.RuleSet.Rules.Count);

                offset = Random.NextInt(0, p1.RuleSet.Rules.Count - p1Segment);
                foreach (var rule in p1.RuleSet.Rules.Skip(offset).Take(p1Segment))
                {
                    rules[rule.Key] = rule.Value;
                }

                offset = Random.NextInt(0, p2.RuleSet.Rules.Count - p2Segment);
                foreach (var rule in p2.RuleSet.Rules.Skip(offset).Take(p2Segment))
                {
                    rules[rule.Key] = rule.Value;
                }                
            } while (rules.Count < size);

            return new RuleSetSpecimen(new RuleSet(rules));
        }
    }
}
