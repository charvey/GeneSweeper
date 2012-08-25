using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm
{
    public class Trial<TType, TSpecimen> where TType:ICloneable where TSpecimen : Specimen<TType> 
    {
        public Population<TType,TSpecimen> Population { get; private set; }
        public List<TType> GenerationBests { get; private set; }
        public List<GenerationScore> GenerationScores { get; private set; }
        public int Generation { get; private set; }

        public readonly TrialConfiguration TrialConfig;

        public readonly string Name;

        public Trial(TrialConfiguration trialConfig)
        {
            TrialConfig = trialConfig;
            Name = DateTime.UtcNow.ToString("yyMMddHHmm");
            Population = new Population<TType, TSpecimen>(trialConfig);
            GenerationBests = new List<TType> { Population.GetBest() };
            GenerationScores=new List<GenerationScore>{Population.GetScore()};
            Generation = 0;

            SaveState();
        }

        public void Evolve()
        {
            Population.Evolve();

            GenerationBests.Add(Population.GetBest());
            GenerationScores.Add(Population.GetScore());
            Generation++;

            SaveState();
        }

        #region State Savers

        private void SaveState()
        {
            SavePopulation();
            SaveScore();
            SaveBest();
        }

        private void SavePopulation()
        {
            File.Delete(FilePath("CurrentPopulation"));
            File.WriteAllLines("CurrentPopulation","All of the RuleSets".Select(c=>""+c).ToArray());
        }

        private void SaveScore()
        {
            File.AppendAllText(FilePath("Scores"), Generation + "\n");
        }

        private void SaveBest()
        {
            File.AppendAllText(FilePath("Bests"),Generation+"\n");
        }

        private string FilePath(string name)
        {
            return Name + "/" + name;
        }

        #endregion

        public static Trial<TType,TSpecimen> Load(string name){
            throw new NotImplementedException();
        }
    }
}
