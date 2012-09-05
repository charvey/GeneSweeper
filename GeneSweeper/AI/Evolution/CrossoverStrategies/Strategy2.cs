using GeneSweeper.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Random = GeneticAlgorithm.Random;

namespace GeneSweeper.AI.Evolution.CrossoverStrategies
{
    public class Strategy2 : ICrossoverStrategy
    {
        public RuleSetSpecimen Crossover(RuleSetSpecimen p1, RuleSetSpecimen p2)
        {
            int before = (p2.RuleSet.Rules.Count + p1.RuleSet.Rules.Count) / 2;

            var offspringRules = new Dictionary<NeighborhoodState, CellState>();

            var keys = p1.RuleSet.Rules.Keys.Union(p2.RuleSet.Rules.Keys).ToList();


            foreach (var key in p1.RuleSet.Rules.Keys.Except(p2.RuleSet.Rules.Keys))
            {
                offspringRules.Add(key, p1.RuleSet.Rules[key]);
            }

            foreach (var key in p2.RuleSet.Rules.Keys.Except(p1.RuleSet.Rules.Keys))
            {
                offspringRules.Add(key, p2.RuleSet.Rules[key]);
            }

            foreach (var key in p1.RuleSet.Rules.Keys.Intersect(p2.RuleSet.Rules.Keys))
            {
                offspringRules.Add(key, (Random.NextBool() ? p1 : p2).RuleSet.Rules[key]);
            }

            while (offspringRules.Count > before)
            {
                offspringRules.Remove(offspringRules.Keys.ElementAt((int)(Random.NextDouble() * offspringRules.Count)));
            }

            return new RuleSetSpecimen(new RuleSet(offspringRules));
        }
    }
}
