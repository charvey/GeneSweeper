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

            const int rules = 100000;

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

            _fitness = Random.NextUlong();

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
            byte[] bytes = new byte[8*v.RuleSet.Rules.Keys.Count];

            int i = 0;
            foreach (var rule in v.RuleSet.Rules)
            {
                ulong r = ((rule.Key.Value << 10) | (((ulong) rule.Value.Value) << 4));
                var b = BitConverter.GetBytes(r);

                bytes[i + 0] = b[0];
                bytes[i + 1] = b[1];
                bytes[i + 2] = b[2];
                bytes[i + 3] = b[3];
                bytes[i + 4] = b[4];
                bytes[i + 5] = b[5];
                bytes[i + 6] = b[6];
                bytes[i + 7] = b[7];

                i += 8;
            }

            return Convert.ToBase64String(bytes);
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
