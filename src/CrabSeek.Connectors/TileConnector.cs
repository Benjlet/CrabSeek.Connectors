namespace CrabSeek.Connectors
{
    internal class TileConnector : Tile
    {
        public TileConnector(int x, int y, string name, byte cost) : base(x, y, name, cost)
        {
        }

        public override TileType Type => TileType.Connector;
    }
}