using System;
using GeneSweeper.AI;
using GeneSweeper.Game;
using GeneSweeper.Game.Players;
using GeneticAlgorithm;

namespace GeneSweeper
{
    public class RuleSetSpecimen:ISpecimen
    {
        public RuleSet RuleSet;

        public RuleSetSpecimen()
        {
            //TODO Generate random ruleset
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
