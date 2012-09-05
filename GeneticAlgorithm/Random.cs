namespace GeneticAlgorithm
{
    public static class Random
    {
        #region Fields

        private static System.Random _rand;
        private static System.Random Rand {get { return _rand ?? (_rand = new System.Random()); }}

        #endregion

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

        #region Bools

        public static bool NextBool()
        {
            return Random.NextDouble() > .5;
        }

        public static bool[] NextBools(int count)
        {
            bool[] bools = new bool[count];

            for (int i = 0; i < count; i++)
            {
                bools[i] = Random.NextDouble() > .5;
            }

            return bools;
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
            byte[] bytes = new byte[8];
            Rand.NextBytes(bytes);

            ulong next = 0;
            next = next | bytes[0];

            next <<= 8;
            next = next | bytes[1];

            next <<= 8;
            next = next | bytes[2];

            next <<= 8;
            next = next | bytes[3];

            next <<= 8;
            next = next | bytes[4];

            next <<= 8;
            next = next | bytes[5];

            next <<= 8;
            next = next | bytes[6];

            next <<= 8;
            next = next | bytes[7];

            return next;
        }

        #endregion

        #region Doubles

        public static double NextDouble()
        {
            return Rand.NextDouble();
        }

        #endregion
    }
}
