namespace CrabSeek.Connectors.Tests
{
    internal class TestTile : ITile
    {
        public TestTile(int x, int y, string name, byte cost)
        {
            X = x;
            Y = y;
            Name = name;
            Cost = cost;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public byte Cost { get; set; }
        public string Name { get; set; }
        public TileType Type { get; set; } = TileType.Room;
    }
}
