
namespace GeneSweeper.AI
{
    public struct Rule
    {
        public readonly ulong Value;

        public Rule(ulong value)
        {
            Value = value;
        }

        public Rule(NeighborhoodState pattern, CellState result)
        {
            Value = ((pattern.Value << 6) | result.Value) << 4;
        }

        public NeighborhoodState Pattern { get { return new NeighborhoodState(Value >> (4 + 6)); } }
        public CellState Result { get { return new CellState((byte) ((Value >> 4) & 63)); } }

        public static Rule GetRandom()
        {
            return new Rule(NeighborhoodState.GetRandom(),CellState.GetRandom());
        }
    }
}
