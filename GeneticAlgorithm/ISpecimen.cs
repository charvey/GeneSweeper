using System;

namespace GeneticAlgorithm
{
    public interface ISpecimen
    {
        ulong Fitness();
        void Mutate();
        ISpecimen Crossover(ISpecimen other);
    }
}
