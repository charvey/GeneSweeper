using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class Population<TSpecimen> where TSpecimen:ISpecimen,new()
    {
        private TSpecimen[] _population;
        private readonly TrialConfiguration<TSpecimen> _configuration;

        private ulong? _totalFitness;
        private ulong TotalFitness
        {
            get
            {
                if (_totalFitness.HasValue)
                    return _totalFitness.Value;

                _totalFitness = 0;

                foreach (var specimen in _population)
                {
                    _totalFitness += specimen.Fitness();
                }

                return _totalFitness.Value;
            }

        } 

        public Population(TrialConfiguration<TSpecimen> trialConfiguration)
        {
            _configuration = trialConfiguration;
            
            Initialize();
            Order();
        }

        #region Public Accessors

        public TSpecimen GetBest()
        {
            return _population[0];
        }

        public GenerationScore GetScore()
        {
            return new GenerationScore
                       {
                           BestScore = _population[0].Fitness(),
                           MeanScore = TotalFitness/((ulong) _configuration.PopulationSize),
                           WorstScore = _population[_configuration.PopulationSize - 1].Fitness(),
                           Percentiles = Enumerable.Range(1, GenerationScore.PercentileSegments - 1)
                               .Select(p => p*(_configuration.PopulationSize/GenerationScore.PercentileSegments))
                               .Select(i => _population[i].Fitness())
                               .ToArray()
                       };
        }

        public string[] StringValue()
        {
            string[] ret = new string[_configuration.PopulationSize];

            for(int i=0;i<_configuration.PopulationSize;i++)
            {
                ret[i] = _configuration.Stringer.ValueToString(_population[i]);
            }

            return ret;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// It is assumed that the population is ordered before and after this method runs.
        /// </summary>
        /// <param name="configuration"></param>
        public void Evolve()
        {
            throw new NotImplementedException();
            Crossover();
            Mutate();
            Order();
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            _population = new TSpecimen[_configuration.PopulationSize];

            foreach (var i in Enumerable.Range(0,_configuration.PopulationSize))
            {
                _population[i] = new TSpecimen();
            }
        }

        private void Crossover()
        {
            throw new NotImplementedException();
        }

        private void Mutate()
        {
            foreach (var specimen in _population)
            {
                if(Random.NextDouble()<_configuration.MutationRate)
                {
                    specimen.Mutate();
                }
            }
        }

        private void Order()
        {
            _population = _population.OrderBy(s => s.Fitness()).ToArray();
        }

        #endregion
    }
}
