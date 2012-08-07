namespace GeneSweeper
{
    public static class Random
    {
        private static System.Random _rand;
        private static System.Random Rand {get { return _rand ?? (_rand = new System.Random()); }}

        public static int Next(int maxValue)
        {
            return Rand.Next(maxValue);
        }
    }
}
