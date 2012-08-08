namespace GeneSweeper
{
    public struct Rule
    {
        public readonly ulong Value;

        public Rule(ulong value)
        {
            Value = value;
        }

        public NeighborhoodState Pattern { get { return new NeighborhoodState(Value >> (4 + 6)); } }
        public CellState Result { get { return new CellState((byte) ((Value >> 4) & 63)); } }
    }
}
