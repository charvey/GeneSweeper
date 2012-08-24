using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm
{
    public class Trial<TType, TSpecimen> where TType:ICloneable where TSpecimen : Specimen<TType> 
    {
        public Population<TType,TSpecimen> Population { get; private set; }
        public List<TType> BestSpecimens { get; private set; }
        public List<GenerationScore> GenerationScores { get; private set; }

        public readonly TrialConfiguration TrialConfig;

        public readonly string Name;

        public Trial(TrialConfiguration trialConfig)
        {
            TrialConfig = trialConfig;

            Name = DateTime.UtcNow.ToString("yyMMddHHmm");
        }



        public static Trial<TType,TSpecimen> Load(string name){
            throw new NotImplementedException();
        }
    }
}
