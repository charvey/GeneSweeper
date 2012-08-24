using System;

namespace GeneticAlgorithm
{
    public abstract class Specimen<T> where T : ICloneable
    {
        public abstract ulong Fitness();
        public abstract void Mutate();
        public abstract Specimen<T> Crossover(Specimen<T> p1, Specimen<T> p2);

        public abstract T Value();
    }
}
