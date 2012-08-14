namespace GeneSweeper.Game
{
    public struct SquareState
    {
        public readonly byte Value;

        public SquareState(byte value)
        {
            Value = (byte)(value & 63);//TODO Make compile time
        }

        public static SquareState Adj0 = new SquareState(00);
        public static SquareState Adj1 = new SquareState(01);
        public static SquareState Adj2 = new SquareState(02);
        public static SquareState Adj3 = new SquareState(03);
        public static SquareState Adj4 = new SquareState(04);
        public static SquareState Adj5 = new SquareState(05);
        public static SquareState Adj6 = new SquareState(06);
        public static SquareState Adj7 = new SquareState(07);
        public static SquareState Adj8 = new SquareState(08);
        public static SquareState Edge = new SquareState(09);
        public static SquareState Flag = new SquareState(10);
        public static SquareState Safe = new SquareState(11);
    }
}
