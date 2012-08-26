namespace GeneticAlgorithm
{
    public static class Random
    {
        private static System.Random _rand;
        private static System.Random Rand {get { return _rand ?? (_rand = new System.Random()); }}

        #region Bytes

        public static byte NextByte(byte maxValue)
        {
            return (byte)Rand.Next(maxValue);
        }

        public static byte NextByte(byte minValue, byte maxValue)
        {
            return (byte)Rand.Next(minValue, maxValue);
        }

        public static byte[] NextBytes(int count)
        {
            byte[] bytes = new byte[count];

            Rand.NextBytes(bytes);

            return bytes;
        }

        #endregion

        #region Ints

        public static int NextInt(int maxValue)
        {
            return Rand.Next(maxValue);
        }

        public static int NextInt(int minValue, int maxValue)
        {
            return Rand.Next(minValue, maxValue);
        }

        #endregion

        #region Ulong

        public static ulong NextUlong()
        {
            return (((ulong)Rand.Next()) << 32) | ((ulong)Rand.Next());
        }

        #endregion

        #region Doubles

        public static double NextDouble()
        {
            return _rand.NextDouble();
        }

        #endregion
    }
}
