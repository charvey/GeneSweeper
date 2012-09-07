using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneSweeper.AI.Evolution.MutationStrategies
{
    public interface IMutationStrategy
    {
        void Mutate(RuleSetSpecimen specimem, double mutationRate);
    }
}
