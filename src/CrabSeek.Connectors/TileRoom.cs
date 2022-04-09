namespace CrabSeek.Connectors
{
    internal class TileRoom : Tile
    {
        public override TileType Type => TileType.Room;

        public TileRoom(int x, int y, string name, byte cost) : base(x, y, name, cost)
        {
        }
    }
}