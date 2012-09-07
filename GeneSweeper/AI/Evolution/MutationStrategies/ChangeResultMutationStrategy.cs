using GeneSweeper.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Random = GeneticAlgorithm.Random;

namespace GeneSweeper.AI.Evolution.MutationStrategies
{
    public class ChangeResultMutationStrategy:IMutationStrategy
    {
        public void Mutate(RuleSetSpecimen specimen, double mutationRate)
        {
            List<NeighborhoodState> keys = new List<NeighborhoodState>();
            foreach(var key in specimen.RuleSet.Rules.Keys)
            {
                if (Random.NextDouble() < mutationRate)
                    keys.Add(key);
            }
            foreach (var key in keys)
            {
                specimen.RuleSet.Rules[key] = CellState.GetRandom();
            }
        }
    }
}
