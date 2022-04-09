namespace CrabSeek.Connectors
{
    internal class TileConnector : Tile
    {
        public override TileType Type => TileType.Connector;

        public TileConnector(int x, int y) : base(x, y, Constants.TILE_NAME_CONNECTOR)
        {
            Cost = (byte)(int)Type;
        }
    }
}