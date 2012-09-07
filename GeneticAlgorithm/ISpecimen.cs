using System;

namespace GeneticAlgorithm
{
    public interface ISpecimen
    {
        ulong Fitness();
        void Mutate(double mutationRate);
        ISpecimen Crossover(ISpecimen other);
    }
}
