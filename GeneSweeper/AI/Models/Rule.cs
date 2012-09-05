
namespace GeneSweeper.AI.Models
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
            Value = (pattern.Value << 10) | (((ulong)result.Value) & 63);
        }

        public NeighborhoodState Pattern { get { return new NeighborhoodState(Value & (ulong.MaxValue - 1023)); } }
        public CellState Result { get { return new CellState((byte)(Value & 63)); } }

        public static Rule GetRandom()
        {
            return new Rule(NeighborhoodState.GetRandom(),CellState.GetRandom());
        }
    }
}
