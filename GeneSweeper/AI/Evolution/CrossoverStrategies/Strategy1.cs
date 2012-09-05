using GeneSweeper.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Random = GeneticAlgorithm.Random;

namespace GeneSweeper.AI.Evolution.CrossoverStrategies
{
    public class Strategy1:ICrossoverStrategy
    {
        public RuleSetSpecimen Crossover(RuleSetSpecimen p1, RuleSetSpecimen p2)
        {
            int before = (p2.RuleSet.Rules.Count + p1.RuleSet.Rules.Count) / 2;

            var offspring = new RuleSetSpecimen(new RuleSet(new Dictionary<NeighborhoodState, CellState>()));

            var keys = p1.RuleSet.Rules.Keys.Union(p2.RuleSet.Rules.Keys).ToList();

            foreach (var key in keys)
            {
                RuleSet parent;

                if (Random.NextDouble() > .5)
                    parent = p1.RuleSet;
                else
                    parent = p2.RuleSet;

                CellState result;
                if (parent.Rules.TryGetValue(key, out result))
                    offspring.RuleSet.Add(new Rule(key, result));
            }

            return offspring;
        }
    }
}
