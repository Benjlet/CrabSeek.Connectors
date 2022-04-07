namespace CrabSeek.Connectors
{
    public class TileConnector : Tile
    {
        public override TileType Type => TileType.Connector;

        public TileConnector()
        {
            Cost = Constants.TILE_COST_CONNECTOR;
        }

        public TileConnector(int x, int y) : base(x, y, Constants.TILE_NAME_CONNECTOR)
        {
            Cost = Constants.TILE_COST_CONNECTOR;
        }

        public TileConnector(int x, int y, string name) : base(x, y, name)
        {
            Cost = Constants.TILE_COST_CONNECTOR;
        }
    }
}