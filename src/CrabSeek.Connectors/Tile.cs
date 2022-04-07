namespace CrabSeek.Connectors
{
    public abstract class Tile : ITile
    {
        public Tile()
        {
            X = Constants.TILE_DEFAULT_X;
            Y = Constants.TILE_DEFAULT_Y;
            Name = Constants.TILE_NAME_VOID;
        }

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
            Name = Constants.TILE_NAME_VOID;
        }

        public Tile(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public byte Cost { get; set; }
        public string Name { get; set; }
        public virtual TileType Type => TileType.Inaccessible;
    }
}