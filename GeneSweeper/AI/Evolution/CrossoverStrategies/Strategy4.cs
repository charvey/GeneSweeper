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

            int split = (int) Random.NextDouble() * size;

            int p1Offset = Random.NextInt(0, p1.RuleSet.Rules.Count - split);

            //int p2Offset = Random.NextInt(

            throw new NotImplementedException();
        }
    }
}
