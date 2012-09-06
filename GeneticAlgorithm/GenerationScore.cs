using System;
using System.Linq;

namespace GeneticAlgorithm
{
    public class GenerationScore
    {
        public ulong BestScore;
        public ulong MeanScore;
        public ulong WorstScore;

        public ulong[] Percentiles;

        public const int PercentileSegments = 10;

        private static GenerationScoreStringer _stringer;
        public static GenerationScoreStringer Stringer
        {
            get { return _stringer ?? (_stringer = new GenerationScoreStringer()); }
        }
    }

    public class GenerationScoreStringer : IStringer<GenerationScore>
    {
        public string ValueToString(GenerationScore v)
        {
            return (new[] {v.BestScore, v.MeanScore, v.WorstScore})
                .Concat(v.Percentiles)
                .Aggregate("", (ag, val) => ag + '\t' + val.ToString("00000000000000"));
        }

        public GenerationScore StringToValue(string s)
        {
            var vals = s.Split(new[] {'\t'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(v => Convert.ToUInt64(v))
                .ToList();

            return new GenerationScore
                       {
                           BestScore = vals[0],
                           MeanScore = vals[1],
                           WorstScore = vals[2],
                           Percentiles = vals.Skip(3).ToArray()
                       };
        }
    }
}
