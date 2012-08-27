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

        public Trial(string name,IStringer<TSpecimen> stringer)
        {
            if(!Directory.Exists(name))
                throw new FileNotFoundException(name+" is not a valid trial name.");

            Name = name;

            TrialConfig = new TrialConfiguration<TSpecimen>();
            TrialConfig = TrialConfig.ConfigStringer.StringToValue(File.ReadAllText(name + "/Config"));
            TrialConfig.Stringer = stringer;

            Population = new Population<TSpecimen>(TrialConfig,
                                                   File.ReadAllLines(name + "/CurrentPopulation")
                                                       .Select(l => TrialConfig.Stringer.StringToValue(l))
                                                       .ToArray());
            GenerationBests = File.ReadAllLines(name + "/Bests")
                .Select(l => TrialConfig.Stringer.StringToValue(l)).ToList();
            GenerationScores = File.ReadAllLines(name + "/Scores")
                .Select(l => GenerationScore.Stringer.StringToValue(l)).ToList();
            Generation = GenerationScores.Count - 1;
        }

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

        public void Evolve(int generations = 1)
        {
            for (; generations>0; generations--)
            {
                Console.WriteLine(generations+" generations remaining.");

                Population.Evolve();

                GenerationBests.Add(Population.GetBest());
                GenerationScores.Add(Population.GetScore());
                Generation++;
            }

            SaveState();
        }

        #region State Savers

        private void SaveState()
        {
            if (!Directory.Exists(Name))
                Directory.CreateDirectory(Name);

            SaveConfig();
            SavePopulation();
            SaveScore();
            SaveBest();
        }

        private void SaveConfig()
        {
            File.WriteAllText(FilePath("Config"), TrialConfig.ConfigStringer.ValueToString(TrialConfig));
        }

        private void SavePopulation()
        {
            File.WriteAllLines(FilePath("CurrentPopulation"), Population.StringValue());
        }

        private void SaveScore()
        {
            File.WriteAllLines(FilePath("Scores"), GenerationScores.Select(GenerationScore.Stringer.ValueToString));
        }

        private void SaveBest()
        {
            File.WriteAllLines(FilePath("Bests"), GenerationBests.Select(TrialConfig.Stringer.ValueToString));
        }

        private string FilePath(string name)
        {
            return Name + "/" + name;
        }

        #endregion
    }
}
