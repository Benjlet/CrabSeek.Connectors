namespace CrabSeek.Connectors
{
    /// <summary>
    /// The type of Tile, represented by a integer code.
    /// </summary>
    public enum TileType
    {
        /// <summary>
        /// Represents an inaccessible tile, as if the move cost was 0.
        /// </summary>
        Inaccessible = 0,

        /// <summary>
        /// Represents a connector between rooms.
        /// </summary>
        Connector = 1,

        /// <summary>
        /// Represents a basic room.
        /// </summary>
        Room = 2
    }
}