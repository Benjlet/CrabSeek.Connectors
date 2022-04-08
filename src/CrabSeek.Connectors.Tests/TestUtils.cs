using System;
using System.Linq;
using System.Text;

namespace CrabSeek.Connectors.Tests
{
    internal class TestUtils
    {
        public static void PrintGridLines(string[] grid)
        {
            Console.WriteLine("╔" + new string('═', (grid.Count() > 0 ? grid.Max(s => s.Length) : 0) + 2) + "╗");

            for (int i = 0; i < grid.Length; i++)
                Console.WriteLine($"║ {grid[i]} ║");

            Console.WriteLine("╚" + new string('═', (grid.Count() > 0 ? grid.Max(s => s.Length) : 0) + 2) + "╝");
        }

        public static void PrintGrid(TileGrid grid)
        {
            Console.WriteLine("\nGRID:");
            PrintGridLines(grid.ToStringArray());

            Console.WriteLine("\nBASE2 BYTE GRID:\n");
            PrintByteGrid(grid.ToBase2ByteGrid());
        }

        public static void PrintByteGrid(byte[,] byteGrid)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < byteGrid.GetLength(0); i++)
            {
                for (int j = 0; j < byteGrid.GetLength(0); j++)
                {
                    sb.Append(byteGrid[i, j]);
                }

                sb.AppendLine();
            }

            var gridLines = sb.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            PrintGridLines(gridLines);
        }
    }
}
