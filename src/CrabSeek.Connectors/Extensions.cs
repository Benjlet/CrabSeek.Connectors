namespace CrabSeek.Connectors
{
    internal static class Extensions
    {
        internal static bool HasXY(this ITile tile, XY xy) => tile.X == xy.X && tile.Y == xy.Y;
    }
}
