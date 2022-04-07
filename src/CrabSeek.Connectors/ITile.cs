namespace CrabSeek.Connectors
{
    public interface ITile
    {
        int X { get; set; }
        int Y { get; set; }
        byte Cost { get; set; }
        string Name { get; set; }
        TileType Type { get; }
    }
}