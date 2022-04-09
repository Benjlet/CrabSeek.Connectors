namespace CrabSeek.Connectors
{
    internal struct XY
    {
        internal XY(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal int X { get; set; }
        internal int Y { get; set; }
    }
}