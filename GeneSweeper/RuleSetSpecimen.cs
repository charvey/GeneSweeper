using System;
using GeneSweeper.AI;
using GeneSweeper.Game;
using GeneSweeper.Game.Players;
using GeneticAlgorithm;
using Random = GeneticAlgorithm.Random;

namespace GeneSweeper
{
    public class RuleSetSpecimen:ISpecimen
    {
        public RuleSet RuleSet;

        public RuleSetSpecimen()
        {
            RuleSet = new RuleSet();

            const int rules = 1000000;

            byte[] buffer = Random.NextBytes(10*rules);

            for (int i = 0; i < rules; i++)
                RuleSet.Add(new Rule(
                                new NeighborhoodState(
                                    (byte) (buffer[10*i + 0] & 63),
                                    (byte) (buffer[10*i + 1] & 63),
                                    (byte) (buffer[10*i + 2] & 63),
                                    (byte) (buffer[10*i + 3] & 63),
                                    (byte) (buffer[10*i + 4] & 63),
                                    (byte) (buffer[10*i + 5] & 63),
                                    (byte) (buffer[10*i + 6] & 63),
                                    (byte) (buffer[10*i + 7] & 63),
                                    (byte) (buffer[10*i + 8] & 63)
                                    ),
                                new CellState((byte)(buffer[10 * i + 9] & 63))));
        }

        public RuleSetSpecimen(RuleSet ruleSet)
        {
            RuleSet = ruleSet;
        }

        private ulong? _fitness;
        public ulong Fitness()
        {
            if(_fitness.HasValue)
                return _fitness.Value;

            //TODO Implement fitness

            _fitness = 0;

            return _fitness.Value;
        }

        public void Mutate()
        {
            //TODO Implement mutate
            _fitness = null;
        }

        public ISpecimen Crossover(ISpecimen other)
        {
            //TODO Implement crossover
            return null;
        }

        public RuleSet Value()
        {
            return RuleSet;
        }
    }

    public class RuleSetSpecimenStringer:IStringer<RuleSetSpecimen>
    {
        public string ValueToString(RuleSetSpecimen v)
        {
            string str = "";

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";

            foreach (var rule in v.RuleSet.Rules)
            {
                ulong r = ((rule.Key.Value << 10) | (((ulong) rule.Value.Value) << 4));

                str += Convert.ToBase64String(BitConverter.GetBytes(r));
            }

            return str;
        }

        public RuleSetSpecimen StringToValue(string s)
        {
            var bytes = Convert.FromBase64String(s);

            var set = new RuleSet();

            for(int i=0;i<bytes.Length;i+=8)
            {
                set.Add(new Rule(BitConverter.ToUInt64(bytes, i)));
            }

            return new RuleSetSpecimen(set);
        }
    }
}
