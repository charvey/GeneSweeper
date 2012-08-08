namespace GeneSweeper
{
    public struct Rule
    {
        private ulong value;

        public ulong NeighborState { get { return value >> (2 + 6); } }
        public byte ResultState { get { return (byte) ((value >> 2) & 63); } }
    }
}
