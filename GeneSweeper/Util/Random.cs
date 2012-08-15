using GeneSweeper.AI;
using GeneSweeper.Game;

namespace GeneSweeper.Util
{
    public static class Random
    {
        private static System.Random _rand;
        private static System.Random Rand {get { return _rand ?? (_rand = new System.Random()); }}

        public static byte NextByte(byte maxValue)
        {
            return (byte)Rand.Next(maxValue);
        }

        public static byte NextByte(byte minValue, byte maxValue)
        {
            return (byte)Rand.Next(minValue, maxValue);
        }

        public static int NextInt(int maxValue)
        {
            return Rand.Next(maxValue);
        }

        public static int NextInt(int minValue, int maxValue)
        {
            return Rand.Next(minValue, maxValue);
        }

        public static ulong NextUlong()
        {
            return (((ulong) Rand.Next()) << 32) | ((ulong) Rand.Next());
        }

        public static CellState NextCellState()
        {
            return new CellState(NextByte(64));
        }

        public static Rule NextRule()
        {
            NeighborhoodState nState = new NeighborhoodState();

            

            //result = result << 4;

            return new Rule(0);
        }
    }
}
