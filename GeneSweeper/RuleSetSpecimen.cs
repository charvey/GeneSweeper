using System;
using System.Linq;
using GeneSweeper.AI;
using GeneSweeper.Game;
using GeneSweeper.Game.Boards;
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
                                new CellState(
                                    (byte) (buffer[10*i + 9] & 63)
                                    )));
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

            _fitness = 0;

            foreach(var i in Enumerable.Range(0,1000))
            {
                Board board = new AutoBoard(Board.Difficulty.Beginner);
                Player player = new SmartPlayer(RuleSet, board);
                player.Play();

                ulong f = (board.CurrentState == Board.State.Won)
                              ? 3u
                              : (board.CurrentState == Board.State.Lost) ? 0u : 2u;
                f = f << 32;

                ushort mines=0;
                ushort positions = 0;

                for (byte r = 0; r < board.CurrentDifficulty.Height; r++)
                {
                    for (byte c = 0; c < board.CurrentDifficulty.Width; c++)
                    {
                        Board.Position p = new Board.Position(r, c);
                        Board.Square s = board[p];

                        if (s.Mine && !s.Revealed)
                        {
                            mines++;
                            positions++;
                        }
                        if (s.Revealed && !s.Mine)
                        {
                            positions++;
                        }
                    }
                }

                f = f | ((ulong)mines << 16) | positions;

                _fitness += f;
            }

            return _fitness.Value;
        }

        public void Mutate()
        {
            int count = RuleSet.Rules.Count;

            byte[] bytes = Random.NextBytes(count/(8*10));

            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((b & 1) == 1)
                    {
                        RuleSet.Add(Rule.GetRandom());
                    }
                    else
                    {
                        RuleSet.Remove(RuleSet.Rules.Keys.ElementAt((int) Random.NextDouble()*RuleSet.Rules.Count));
                    }

                    b >>= 1;
                }
            }

            _fitness = null;
        }

        public ISpecimen Crossover(ISpecimen other)
        {
            RuleSetSpecimen casted = other as RuleSetSpecimen;
            RuleSet offspring = new RuleSet();

            var keys = this.RuleSet.Rules.Keys.Union(casted.RuleSet.Rules.Keys);

            foreach (var key in keys)
            {
                RuleSet parent;

                if(Random.NextDouble()>.5)
                    parent = this.RuleSet;
                else
                    parent = casted.RuleSet;

                CellState result;
                if (parent.Rules.TryGetValue(key, out result))
                    offspring.Add(new Rule(key, result));
            }
            
            return new RuleSetSpecimen(offspring);
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
