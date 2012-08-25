using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class Population<TType,TSpecimen> where TType:ICloneable where TSpecimen:Specimen<TType>
    {
        private TSpecimen[] _population;
        private readonly TrialConfiguration _configuration;

        public Population(TrialConfiguration trialConfiguration)
        {
            _configuration = trialConfiguration;
            _population = new TSpecimen[_configuration.PopulationSize];
            Order();
        }

        #region Public Accessors

        public TType GetBest()
        {
            throw new NotImplementedException();
        }

        public GenerationScore GetScore()
        {
            throw new NotImplementedException();
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

        private void Crossover()
        {
            throw new NotImplementedException();
        }

        private void Mutate()
        {
            foreach (var specimen in _population.AsParallel())
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
