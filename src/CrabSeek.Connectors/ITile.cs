namespace CrabSeek.Connectors
{
    /// <summary>
    /// Interface for implementing a Tile, including details of its position and cost.
    /// </summary>
    public interface ITile
    {
        /// <summary>
        /// X-position of this tile.
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// Y-position of this tile.
        /// </summary>
        int Y { get; set; }

        /// <summary>
        /// The cost of moving to this tile, to be used with heuristic functions.
        /// </summary>
        byte Cost { get; set; }

        /// <summary>
        /// The name reference of this tile.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The type of this tile, such as a Room or Connector.
        /// </summary>
        TileType Type { get; set; }
    }
}