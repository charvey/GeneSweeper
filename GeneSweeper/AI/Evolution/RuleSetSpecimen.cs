﻿using GeneSweeper.AI.Evolution.CrossoverStrategies;
using GeneSweeper.AI.Models;
using GeneSweeper.Game;
using GeneSweeper.Game.Boards;
using GeneSweeper.Game.Players;
using GeneticAlgorithm;
using System;
using System.Linq;
using Random = GeneticAlgorithm.Random;

namespace GeneSweeper.AI.Evolution
{
    public class RuleSetSpecimen : ISpecimen
    {
        public RuleSet RuleSet;

        public RuleSetSpecimen()
        {
            RuleSet = new RuleSet();

            const int rules = 100000;

            byte[] buffer = Random.NextBytes(10 * rules);

            for (int i = 0; i < rules; i++)
                RuleSet.Add(new Rule(
                                new NeighborhoodState(
                                    (byte)(buffer[10 * i + 0] & 63),
                                    (byte)(buffer[10 * i + 1] & 63),
                                    (byte)(buffer[10 * i + 2] & 63),
                                    (byte)(buffer[10 * i + 3] & 63),
                                    (byte)(buffer[10 * i + 4] & 63),
                                    (byte)(buffer[10 * i + 5] & 63),
                                    (byte)(buffer[10 * i + 6] & 63),
                                    (byte)(buffer[10 * i + 7] & 63),
                                    (byte)(buffer[10 * i + 8] & 63)
                                    ),
                                new CellState(
                                    (byte)(buffer[10 * i + 9] & 63)
                                    )));
        }

        public RuleSetSpecimen(RuleSet ruleSet)
        {
            RuleSet = ruleSet;
        }

        public static int calls=0;

        private ulong? _fitness;
        public ulong Fitness()
        {
            if (_fitness.HasValue)
                return _fitness.Value;

            //calls++;
            //Console.WriteLine("S:" + calls);

            _fitness = 0;

            foreach (var i in Enumerable.Range(0,1024))
            {
                _fitness += SingleFitness();
            }

            //Console.WriteLine("E:" + calls);
            return _fitness.Value;
        }

        private ulong SingleFitness()
        {
            Board board = new AutoBoard(Board.Difficulty.Beginner);
            Player player = new SmartPlayer(RuleSet, board);
            player.Play();

            ulong f = (board.CurrentState == Board.State.Won)
                          ? 3u
                          : (board.CurrentState == Board.State.Lost) ? 0u : 2u;
            f = f << 32;

            ushort mines = 0;
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

            return f;
        }

        public static int mDiff=0;
        public void Mutate()
        {
            int before = RuleSet.Rules.Count;

            int count = RuleSet.Rules.Count;

            bool[] bools = Random.NextBools(count / 10);

            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i])
                {
                    RuleSet.Add(Rule.GetRandom());
                }
                else
                {
                    RuleSet.Remove(RuleSet.Rules.Keys.ElementAt((int) Random.NextDouble()*RuleSet.Rules.Count));
                }
            }

            int after = RuleSet.Rules.Count;

            mDiff += (after - before);
            //Console.WriteLine("M\tB: " + before + "\tA: " + after + "\tD: " + (after - before) + "\tT:" + mDiff);
            _fitness = null;
        }

        private static ICrossoverStrategy CrossoverStrategy = new Strategy4();
        public static int cDiff=0;
        public ISpecimen Crossover(ISpecimen other)
        {
            int before = (RuleSet.Rules.Count + ((RuleSetSpecimen)other).RuleSet.Rules.Count) / 2;

            RuleSetSpecimen result = CrossoverStrategy.Crossover((RuleSetSpecimen)this, (RuleSetSpecimen)other);

            int after = result.RuleSet.Rules.Count;
            
            cDiff += (after - before);
            //Console.WriteLine("C\tB: " + before + "\tA: " + after + "\tD: " + (after - before) + "\tT:" + cDiff);

            return result;
        }

        public RuleSet Value()
        {
            return RuleSet;
        }
    }

    public class RuleSetSpecimenStringer : IStringer<RuleSetSpecimen>
    {
        public string ValueToString(RuleSetSpecimen v)
        {
            byte[] bytes = new byte[8 * v.RuleSet.Rules.Keys.Count];

            int i = 0;
            foreach (var rule in v.RuleSet.Rules)
            {
                ulong r = ((rule.Key.Value << 10) | (((ulong)rule.Value.Value) << 4));
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

            for (int i = 0; i < bytes.Length; i += 8)
            {
                set.Add(new Rule(BitConverter.ToUInt64(bytes, i)));
            }

            return new RuleSetSpecimen(set);
        }
    }
}
