namespace CrabSeek.Connectors
{
    public class TileRoom : Tile
    {
        public override TileType Type => TileType.Room;

        public TileRoom()
        {
            Cost = Constants.TILE_COST_ROOM;
        }

        public TileRoom(int x, int y) : base(x, y, Constants.TILE_NAME_ROOM)
        {
            Cost = Constants.TILE_COST_ROOM;
        }

        public TileRoom(int x, int y, string name) : base(x, y, name)
        {
            Cost = Constants.TILE_COST_ROOM;
        }
    }
}