using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneSweeper.Util;
using Random = GeneticAlgorithm.Random;
using GeneSweeper.AI.Models;

namespace GeneSweeper.AI.Evolution.CrossoverStrategies
{
    public class Strategy3 : ICrossoverStrategy
    {
        public RuleSetSpecimen Crossover(RuleSetSpecimen p1, RuleSetSpecimen p2)
        {
            int before = (p2.RuleSet.Rules.Count + p1.RuleSet.Rules.Count) / 2;

            var offspringRules = new Dictionary<NeighborhoodState, CellState>();

            var keys = p1.RuleSet.Rules.Keys.Union(p2.RuleSet.Rules.Keys).ToList();

            for (int i = 0; i < before; i++)
            {
                var key = keys.RandomElement(true);
                CellState val1, val2;

                if (p1.RuleSet.Rules.TryGetValue(key, out val1))
                {
                    if (p2.RuleSet.Rules.TryGetValue(key, out val2))
                    {
                        offspringRules.Add(key, (Random.NextBool() ? p1 : p2).RuleSet.Rules[key]);
                    }
                    else
                    {
                        offspringRules.Add(key, val1);
                    }
                }
                else
                {
                    offspringRules.Add(key, p2.RuleSet.Rules[key]);
                }
            }

            return new RuleSetSpecimen(new RuleSet(offspringRules));
        }
    }
}
