using System;

namespace GeneticAlgorithm
{
    public class TrialConfiguration<TSpecimen> where TSpecimen:ISpecimen
    {
        public int PopulationSize;
        public double MutationRate;
        public double CarryoverRate;

        public IStringer<TSpecimen> Stringer;

        public TrialConfigurationStringer<TSpecimen> ConfigStringer = new TrialConfigurationStringer<TSpecimen>();
    }

    public class TrialConfigurationStringer<TSpecimen> : IStringer<TrialConfiguration<TSpecimen>> where TSpecimen:ISpecimen
    {
        public string ValueToString(TrialConfiguration<TSpecimen> v)
        {
            return v.PopulationSize + "\t" + v.MutationRate + "\t" + v.CarryoverRate;
        }

        public TrialConfiguration<TSpecimen> StringToValue(string s)
        {
            var vals = s.Split(new[] {'\t'}, StringSplitOptions.RemoveEmptyEntries);

            return new TrialConfiguration<TSpecimen>
                       {
                           PopulationSize = int.Parse(vals[0]),
                           MutationRate = double.Parse(vals[1]),
                           CarryoverRate = double.Parse(vals[2])
                       };
        }
    }
}
