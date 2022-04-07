namespace CrabSeek.Connectors
{
    internal struct XY
    {
        public XY()
        {
        }

        public XY(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
    }
}