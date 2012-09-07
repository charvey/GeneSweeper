using GeneSweeper.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Random = GeneticAlgorithm.Random;

namespace GeneSweeper.AI.Evolution.MutationStrategies
{
    public class AddRemoveMutationStrategy:IMutationStrategy
    {
        public void Mutate(RuleSetSpecimen specimen, double mutationRate)
        {
            int count = specimen.RuleSet.Rules.Count;

            bool[] bools = Random.NextBools((int)(count*mutationRate));

            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i])
                {
                    specimen.RuleSet.Add(Rule.GetRandom());
                }
                else
                {
                    specimen.RuleSet.Remove(specimen.RuleSet.Rules.Keys.ElementAt((int)Random.NextDouble() * specimen.RuleSet.Rules.Count));
                }
            }
        }
    }
}
