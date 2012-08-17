namespace GeneSweeper.AI
{
    public struct NeighborhoodState
    {
        public readonly ulong Value;

        public NeighborhoodState(ulong value)
        {
            Value = value >> (4 + 6);//TODO Make compile time
        }

        public NeighborhoodState(byte tl, byte tc, byte tr, byte ml, byte mc, byte mr,byte bl,byte bc,byte br)
        {
            ulong x = 0, min = 0;
            //Left to Right, Top to Bottom
            //((((((((((((((((((ulong)tl) << 6) | tc) << 6) | tr) << 6) | ml) << 6) | mc) << 6) | mr) << 6) | bl) << 6) | bc) << 6) | br);

            //Top Left, Clockwise
            x=((((((((((((((((((ulong)tl) << 6) | tc) << 6) | tr) << 6) | mr) << 6) | br) << 6) | bc) << 6) | bl) << 6) | ml) << 6) | mc);
            if (x < min)
                min = x;
            //Top Left, Counter Clockwise
            x = ((((((((((((((((((ulong)tl) << 6) | ml) << 6) | bl) << 6) | bc) << 6) | br) << 6) | mr) << 6) | tr) << 6) | tc) << 6) | mc);
            if (x < min)
                min = x;
            //Top Right, Clockwise
            x = ((((((((((((((((((ulong)tr) << 6) | mr) << 6) | br) << 6) | bc) << 6) | bl) << 6) | ml) << 6) | tl) << 6) | tc) << 6) | mc);
            if (x < min)
                min = x;
            //Top Right, Counter Clockwise
            x = ((((((((((((((((((ulong)tr) << 6) | tc) << 6) | tl) << 6) | ml) << 6) | bl) << 6) | bc) << 6) | br) << 6) | mr) << 6) | mc);
            if (x < min)
                min = x;
            //Bottom Left, Clockwise
            x = ((((((((((((((((((ulong)bl) << 6) | ml) << 6) | tl) << 6) | tc) << 6) | tr) << 6) | mr) << 6) | br) << 6) | bc) << 6) | mc);
            if (x < min)
                min = x;
            //Bottom Left, Counter Clockwise
            x = ((((((((((((((((((ulong)bl) << 6) | bc) << 6) | br) << 6) | mr) << 6) | tr) << 6) | tc) << 6) | tl) << 6) | ml) << 6) | mc);
            if (x < min)
                min = x;
            //Bottom Right, Clockwise
            x = ((((((((((((((((((ulong)br) << 6) | bc) << 6) | bl) << 6) | ml) << 6) | tl) << 6) | tc) << 6) | tr) << 6) | mr) << 6) | mc);
            if (x < min)
                min = x;
            //Bottom Right, Counter Clockwise
            x = ((((((((((((((((((ulong)br) << 6) | mr) << 6) | tr) << 6) | tc) << 6) | tl) << 6) | ml) << 6) | bl) << 6) | bc) << 6) | mc);
            if (x < min)
                min = x;

            Value = min;
        }
    }
}
