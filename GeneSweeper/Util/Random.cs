using GeneSweeper.AI;

namespace GeneSweeper.Util
{
    public static class Random
    {
        private static System.Random _rand;
        private static System.Random Rand {get { return _rand ?? (_rand = new System.Random()); }}

        public static int Next(int maxValue)
        {
            return Rand.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return Rand.Next(minValue, maxValue);
        }

        public static ulong NextUlong()
        {
            return (((ulong) Rand.Next()) << 32) | ((ulong) Rand.Next());
        }

        public static Rule NextRule()
        {
            ulong result = 0;

            for (int i = 0; i < 9; i++)
                result = (result << 6) | ((uint) Next(16));

            result = (result << 6) | ((uint) Next(10, 16));

            result = result << 4;

            return new Rule(result);
        }
    }
}
