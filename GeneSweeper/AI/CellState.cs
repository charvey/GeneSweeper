﻿using GeneticAlgorithm;

namespace GeneSweeper.AI
{
    public struct CellState
    {
        public readonly byte Value;

        public CellState(byte value)
        {
            Value = (byte)((value << 2) >> 2);
        }

        public static CellState Adj0 = new CellState(00);
        public static CellState Adj1 = new CellState(01);
        public static CellState Adj2 = new CellState(02);
        public static CellState Adj3 = new CellState(03);
        public static CellState Adj4 = new CellState(04);
        public static CellState Adj5 = new CellState(05);
        public static CellState Adj6 = new CellState(06);
        public static CellState Adj7 = new CellState(07);
        public static CellState Adj8 = new CellState(08);
        public static CellState Edge = new CellState(09);
        public static CellState Flag = new CellState(10);
        public static CellState Safe = new CellState(11);

        public const byte StateCount = 64; //2^6

        public static CellState GetRandom()
        {
            return new CellState(Random.NextByte(StateCount));
        }
    }
}
