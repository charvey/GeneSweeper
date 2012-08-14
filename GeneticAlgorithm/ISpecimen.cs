namespace GeneticAlgorithm
{
    public interface ISpecimen
    {
        ulong Fitness();
        void Mutate();
    }
}
