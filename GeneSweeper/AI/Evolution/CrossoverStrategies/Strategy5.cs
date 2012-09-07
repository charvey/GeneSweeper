using GeneSweeper.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Random = GeneticAlgorithm.Random;

namespace GeneSweeper.AI.Evolution.CrossoverStrategies
{
    public class Strategy5:ICrossoverStrategy
    {
        public RuleSetSpecimen Crossover(RuleSetSpecimen p1, RuleSetSpecimen p2)
        {
            Dictionary<NeighborhoodState, CellState> rules = new Dictionary<NeighborhoodState, CellState>();
            RuleSetSpecimen t;
            var keys = p1.RuleSet.Rules.Keys.Union(p2.RuleSet.Rules.Keys);

            foreach (var key in keys)
            {
                if (Random.NextBool())
                {
                    t = p1;
                    p1 = p2;
                    p2 = t;
                    
                }

                CellState value;
                if (p1.RuleSet.Rules.TryGetValue(key, out value))
                {
                    rules[key] = value;
                }
                else
                {
                    rules[key] = p2.RuleSet.Rules[key];
                }
            }

            return new RuleSetSpecimen(new RuleSet(rules));
        }
    }
}
