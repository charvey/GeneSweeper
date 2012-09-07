using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeneticAlgorithm
{
    public class Trial<TSpecimen> where TSpecimen:ISpecimen,new()
    {
        #region Fields

        public Population<TSpecimen> Population { get; private set; }
        public TSpecimen Best { get; private set; }
        public List<GenerationScore> GenerationScores { get; private set; }
        public int Generation { get; private set; }

        public readonly TrialConfiguration<TSpecimen> TrialConfig;

        public readonly string Name;

        #endregion

        #region Contructors

        public Trial(string name,IStringer<TSpecimen> stringer)
        {
            if(!Directory.Exists(name))
                throw new FileNotFoundException(name+" is not a valid trial name.");

            Name = name;

            TrialConfig = new TrialConfiguration<TSpecimen>();
            TrialConfig = TrialConfig.ConfigStringer.StringToValue(File.ReadAllText(FilePath("Config")));
            TrialConfig.Stringer = stringer;

            Population = new Population<TSpecimen>(TrialConfig,
                                                   File.ReadAllLines(FilePath("CurrentPopulation"))
                                                       .Select(l => TrialConfig.Stringer.StringToValue(l))
                                                       .ToArray());
            Best = TrialConfig.Stringer.StringToValue(File.ReadAllText(FilePath("Best")));
            GenerationScores = File.ReadAllLines(FilePath("Scores")).Select(l => GenerationScore.Stringer.StringToValue(l)).ToList();
            Generation = GenerationScores.Count - 1;
        }

        public Trial(string name, TrialConfiguration<TSpecimen> trialConfig)
        {
            Name = name;
            TrialConfig = trialConfig;
            
            Population = new Population<TSpecimen>(trialConfig);
            Best = Population.GetBest();
            GenerationScores = new List<GenerationScore> { Population.GetScore() };
            Generation = 0;

            if (Directory.Exists(name))
                Directory.Delete(name, true);

            SaveState();
        }

        #endregion

        #region Public Accessors

        public TSpecimen GetBestLiving()
        {
            return Population.GetBest();
        }

        public TSpecimen GetBestEver()
        {
            return Best;
        }

        #endregion

        #region Public Methods

        public void Evolve(int generations = 1)
        {
            for (; generations>0; generations--)
            {
                Console.WriteLine(generations+" generations remaining.");

                Population.Evolve();

                if (Population.GetBest().Fitness() > Best.Fitness())
                    Best = Population.GetBest();
                GenerationScores.Add(Population.GetScore());
                Generation++;

                if (Generation % 10 == 0)
                    SaveState();
            }

            SaveState();
        }

        #endregion

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
            File.WriteAllText(FilePath("Best"), TrialConfig.Stringer.ValueToString(Best));
        }

        private string FilePath(string name)
        {
            return Name + "/" + name;
        }

        #endregion
    }
}
