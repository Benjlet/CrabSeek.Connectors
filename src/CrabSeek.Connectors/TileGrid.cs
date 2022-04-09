using System.Text;

namespace CrabSeek.Connectors
{
    /// <summary>
    /// A TileGrid used to visualise the positions of a collection of Tiles.
    /// </summary>
    public class TileGrid
    {
        /// <summary>
        /// The collection of Tiles provided at instantiation, offset by the maximum lowest/high X and Y
        /// to construct a Tile collection with compact XY values.
        /// </summary>
        public ITile[,] Tiles { get; }

        /// <summary>
        /// The Width of the Tiles supplied at instantiation - the distance between the minimum/maximum Tile X value.
        /// </summary>
        public int Width { get; } = 0;

        /// <summary>
        /// The Width of the Tiles supplied at instantiation - the distance between the minimum/maximum Tile X value.
        /// </summary>
        public int Height { get; } = 0;

        /// <summary>
        /// Creates a new instance of TileGrid using data from the supplied Tiles.
        /// The Width and Height values are derived from the distance between the lowest/highest X and Y,
        /// so may not match the original XY values after conversion to a grid.
        /// </summary>
        /// <param name="tiles"></param>
        public TileGrid(IEnumerable<ITile> tiles)
        {
            if (tiles == null || !tiles.Any())
            {
                return;
            }

            var minX = tiles.Min(r => r.X);
            var maxX = tiles.Max(r => r.X);
            var minY = tiles.Min(r => r.Y);
            var maxY = tiles.Max(r => r.Y);

            Height = maxY - minY + 1;
            Width = maxX - minX + 1;

            Tiles = new ITile[Height, Width];

            foreach (var tile in tiles)
            {
                tile.X -= minX;
                tile.Y -= minY;
                Tiles[tile.Y, tile.X] = tile;
            }
        }

        /// <summary>
        /// Converts the Tile grid to a single-line string, using the first character of each Tile name.
        /// Will include corridors, if present in the Tile-Collection.
        /// </summary>
        /// <returns>A string representing the first character of each room name in the TileGrid.</returns>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// Converts the TileGrid to a string, containing a NewLine character at the end of each Grid line, 
        /// using the first character of each Tile name. Will include corridors, if present in the Tile-Collection.
        /// Tiles without a connector or room are represented with a space.
        /// </summary>
        /// <param name="withNewLine">Whether to append a NewLine char at the end of each line.</param>
        /// <returns>A string representing the first character of each room name in the TileGrid, rows separated by NewLine characters.</returns>
        public string ToString(bool withNewLine = true)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    sb.Append(Tiles[i, j]?.Name?[..1] ?? Constants.TILE_NAME_VOID);

                if (withNewLine)
                    sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts the TileGrid to a string array, each element representing a new row.
        /// Each character represents the room name at that position. Tiles without a connector or room are represented with a space.
        /// </summary>
        /// <returns>A string array representing the first character of each room name in the TileGrid, each element representing a row.</returns>
        public string[] ToStringArray()
        {
            var gridString = ToString();
            return gridString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Converts the TileGrid to a two-dimensional byte array, each byte representing the cost of that tile at that position.
        /// Tile positions are overlayed on a base-2 sized grid, allowing use with the CrabSeek library to find the lowest-cost path between two points.
        /// The Tiles of this TileGrid instance are centred upon the returned byte grid.
        /// </summary>
        /// <returns>A two-dimensional byte array, representing grid tile costs.</returns>
        public byte[,] ToBase2ByteGrid()
        {
            var gridSize = Util.GetBase2GridSize(Width, Height);
            var grid = new byte[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    grid[i, j] = 0;

            var diffX = Util.GreaterThanZero((int)gridSize, Width) ? ((int)gridSize - Width) / 2 : 0;
            var diffY = Util.GreaterThanZero((int)gridSize, Height) ? ((int)gridSize - Height) / 2 : 0;

            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    grid[i + diffY, j + diffX] = Tiles[i, j]?.Cost ?? 0;

            return grid;
        }
    }
}