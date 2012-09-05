using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class Population<TSpecimen> where TSpecimen:ISpecimen,new()
    {
        #region Data Fields

        private TSpecimen[] _population;
        private readonly TrialConfiguration<TSpecimen> _configuration;

        #endregion

        #region Computed Fields

        private ulong[] CumulativeFitness;
        private ulong TotalFitness;

        #endregion

        #region Constructors

        public Population(TrialConfiguration<TSpecimen> trialConfiguration)
        {
            _configuration = trialConfiguration;
            
            Initialize();
            Order();
        }

        public Population(TrialConfiguration<TSpecimen> trialConfiguration,TSpecimen[] population)
        {
            _configuration = trialConfiguration;
            _population = population;

            Order();
        }

        #endregion

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

        public IEnumerable<string> StringValue()
        {
            for(int i=0;i<_configuration.PopulationSize;i++)
            {
                yield return _configuration.Stringer.ValueToString(_population[i]);
            }
        }

        #endregion

        #region Public Methods

        public void Evolve()
        {
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
            var newPopulation = new TSpecimen[_configuration.PopulationSize];

            int carryoverPoint = (int) (_configuration.CarryoverRate*_configuration.PopulationSize);

            for (int i = 0; i < carryoverPoint; i++)
            {
                newPopulation[i] = _population[i];
            }
            for (int i = carryoverPoint; i < _configuration.PopulationSize; i++)
            {
                ulong p1cf = (ulong)(Random.NextDouble() * TotalFitness),
                    p2cf = (ulong)(Random.NextDouble() * TotalFitness);

                int p1i = Enumerable.Range(0, _configuration.PopulationSize)
                    .First(index => CumulativeFitness[index] >= p1cf);
                int    p2i = Enumerable.Range(0, _configuration.PopulationSize)
                    .First(index => CumulativeFitness[index] >= p2cf);

                if (p1i == p2i)
                    if (p1i == 0)
                        p2i++;
                    else
                        p1i--;

                newPopulation[i] = (TSpecimen) _population[p1i].Crossover(_population[p2i]);
            }

            _population = newPopulation;
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
            //int processorCount = Environment.ProcessorCount*8;

            //var partitions = Enumerable.Range(0, processorCount).Select(p => _population.Where((s, i) => i%processorCount == p));

            //Parallel.ForEach(partitions, p => p.Select(s => s.Fitness()));

            _population = _population.OrderByDescending(s => s.Fitness()).ToArray();

            CumulativeFitness=new ulong[_population.Length];
            TotalFitness = 0;
            for (int i = 0; i < _population.Length;i++ )
            {
                TotalFitness += _population[i].Fitness();
                CumulativeFitness[i] = TotalFitness;
            }
        }

        #endregion
    }
}
