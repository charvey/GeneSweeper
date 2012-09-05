using System;
using Random = GeneticAlgorithm.Random;

namespace GeneSweeper.AI.Models
{
    public struct NeighborhoodState
    {
        public readonly ulong Value;

        public NeighborhoodState(ulong value)
        {
            Value = value;
        }

        public NeighborhoodState(byte tl, byte tc, byte tr, byte ml, byte mc, byte mr,byte bl,byte bc,byte br)
        {
            ulong x = 0, min = ulong.MaxValue;
            //Left to Right, Top to Bottom
            //((((((((((((((((((ulong)tl) << 6) | tc) << 6) | tr) << 6) | ml) << 6) | mc) << 6) | mr) << 6) | bl) << 6) | bc) << 6) | br);

            //Top Left, Clockwise
            x=((((((((((((((((((ulong)tl) << 6) | tc) << 6) | tr) << 6) | mr) << 6) | br) << 6) | bc) << 6) | bl) << 6) | ml) << 6) | mc);
            if (x < min)
                min = x;
            //Top Left, Counter Clockwise
            x = ((((((((((((((((((ulong)tl) << 6) | ml) << 6) | bl) << 6) | bc) << 6) | br) << 6) | mr) << 6) | tr) << 6) | tc) << 6) | mc);
            if (x < min)
                min = x;
            //Top Right, Clockwise
            x = ((((((((((((((((((ulong)tr) << 6) | mr) << 6) | br) << 6) | bc) << 6) | bl) << 6) | ml) << 6) | tl) << 6) | tc) << 6) | mc);
            if (x < min)
                min = x;
            //Top Right, Counter Clockwise
            x = ((((((((((((((((((ulong)tr) << 6) | tc) << 6) | tl) << 6) | ml) << 6) | bl) << 6) | bc) << 6) | br) << 6) | mr) << 6) | mc);
            if (x < min)
                min = x;
            //Bottom Left, Clockwise
            x = ((((((((((((((((((ulong)bl) << 6) | ml) << 6) | tl) << 6) | tc) << 6) | tr) << 6) | mr) << 6) | br) << 6) | bc) << 6) | mc);
            if (x < min)
                min = x;
            //Bottom Left, Counter Clockwise
            x = ((((((((((((((((((ulong)bl) << 6) | bc) << 6) | br) << 6) | mr) << 6) | tr) << 6) | tc) << 6) | tl) << 6) | ml) << 6) | mc);
            if (x < min)
                min = x;
            //Bottom Right, Clockwise
            x = ((((((((((((((((((ulong)br) << 6) | bc) << 6) | bl) << 6) | ml) << 6) | tl) << 6) | tc) << 6) | tr) << 6) | mr) << 6) | mc);
            if (x < min)
                min = x;
            //Bottom Right, Counter Clockwise
            x = ((((((((((((((((((ulong)br) << 6) | mr) << 6) | tr) << 6) | tc) << 6) | tl) << 6) | ml) << 6) | bl) << 6) | bc) << 6) | mc);
            if (x < min)
                min = x;

            Value = min << (6 + 4);
        }

        public static NeighborhoodState GetRandom()
        {
            byte[] bytes = Random.NextBytes(9);

            NeighborhoodState next = new NeighborhoodState(
                (byte)(bytes[0] % CellState.StateCount), (byte)(bytes[1] % CellState.StateCount), (byte)(bytes[2] % CellState.StateCount),
                (byte)(bytes[3] % CellState.StateCount), (byte)(bytes[4] % CellState.StateCount), (byte)(bytes[5] % CellState.StateCount),
                (byte)(bytes[6] % CellState.StateCount), (byte)(bytes[7] % CellState.StateCount), (byte)(bytes[8] % CellState.StateCount));

            if (2573485501887179 == next.Value)
                Console.WriteLine("HERE IS ONE");

            return next;
        }
    }
}
