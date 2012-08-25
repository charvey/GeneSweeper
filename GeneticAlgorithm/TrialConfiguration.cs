namespace GeneticAlgorithm
{
    public class TrialConfiguration<ISpecimen>
    {
        public int PopulationSize;
        public double MutationRate;
        public double CrossoverRate;

        public IStringer<ISpecimen> Stringer;
    }
}
