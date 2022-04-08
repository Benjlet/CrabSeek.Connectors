namespace CrabSeek.Connectors
{
    public class TileGrid
    {
        public ITile[,] Tiles { get; }
        public int Width { get; } = 0;
        public int Height { get; } = 0;

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

            Width = maxX - minX + 1;
            Height = maxY - minY + 1;

            Tiles = new ITile[Height, Width];

            foreach (var tile in tiles)
            {
                tile.X -= minX;
                tile.Y -= minY;
                Tiles[tile.Y, tile.X] = tile;
            }
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    sb.Append(Tiles[i, j]?.Name?[..1] ?? " ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public string[] ToStringArray()
        {
            var gridString = ToString();
            return gridString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        public byte[,] ToBase2ByteGrid()
        {
            var gridSize = System.Numerics.BitOperations.RoundUpToPowerOf2((uint)(Width > Height ? Width : Height));

            var grid = new byte[gridSize, gridSize];

            // Initialise all cells with 0.
            for (int i = 0; i < gridSize; i++)
                for (int j = 0; j < gridSize; j++)
                    grid[i, j] = 0;

            // Overlay actual grid costs.
            // TODO: Position this at centre of array.
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    grid[i, j] = Tiles[i, j]?.Cost ?? 0;

            return grid;
        }
    }
}