namespace CrabSeek.Connectors
{
    internal class TileRoom : Tile
    {
        public override TileType Type => TileType.Room;

        public TileRoom(int x, int y) : base(x, y, Constants.TILE_NAME_ROOM)
        {
            Cost = (byte)(int)Type;
        }
    }
}