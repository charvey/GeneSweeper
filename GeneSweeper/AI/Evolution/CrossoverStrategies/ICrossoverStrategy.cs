using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneSweeper.AI.Evolution.CrossoverStrategies
{
    public interface ICrossoverStrategy
    {
        RuleSetSpecimen Crossover(RuleSetSpecimen p1, RuleSetSpecimen p2);
    }
}
