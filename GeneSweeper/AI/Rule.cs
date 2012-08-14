using GeneSweeper.Game;

namespace GeneSweeper.AI
{
    public struct Rule
    {
        public readonly ulong Value;

        public Rule(ulong value)
        {
            Value = value;
        }

        public NeighborhoodState Pattern { get { return new NeighborhoodState(Value >> (4 + 6)); } }
        public SquareState Result { get { return new SquareState((byte) ((Value >> 4) & 63)); } }
    }
}
