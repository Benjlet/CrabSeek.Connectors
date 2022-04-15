namespace CrabSeek.Connectors
{
    internal class TileRoom : Tile
    {
        public TileRoom(int x, int y, string name, byte cost) : base(x, y, name, cost)
        {
        }

        public override TileType Type => TileType.Room;
    }
}