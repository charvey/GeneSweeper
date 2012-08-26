using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm
{
    public class Trial<TSpecimen> where TSpecimen:ISpecimen,new()
    {
        public Population<TSpecimen> Population { get; private set; }
        public List<TSpecimen> GenerationBests { get; private set; }
        public List<GenerationScore> GenerationScores { get; private set; }
        public int Generation { get; private set; }

        public readonly TrialConfiguration<TSpecimen> TrialConfig;

        public readonly string Name;

        public Trial(string name, TrialConfiguration<TSpecimen> trialConfig)
        {
            Name = name;
            TrialConfig = trialConfig;
            
            Population = new Population<TSpecimen>(trialConfig);
            GenerationBests = new List<TSpecimen> { Population.GetBest() };
            GenerationScores = new List<GenerationScore> { Population.GetScore() };
            Generation = 0;

            if (Directory.Exists(name))
                Directory.Delete(name, true);

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
            if (!Directory.Exists(Name))
                Directory.CreateDirectory(Name);

            SavePopulation();
            SaveScore();
            SaveBest();
        }

        private void SavePopulation()
        {
            File.WriteAllLines(FilePath("CurrentPopulation"), Population.StringValue());
        }

        private void SaveScore()
        {
            File.AppendAllText(FilePath("Scores"), GenerationScore.Stringer.ValueToString(GenerationScores[Generation]));
        }

        private void SaveBest()
        {
            File.AppendAllText(FilePath("Bests"), TrialConfig.Stringer.ValueToString(GenerationBests[Generation]));
        }

        private string FilePath(string name)
        {
            return Name + "/" + name;
        }

        #endregion

        public static Trial<TSpecimen> Load(string name, IStringer<TSpecimen> stringer)
        {
            throw new NotImplementedException();
        }
    }
}
