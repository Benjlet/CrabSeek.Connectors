using NUnit.Framework;
using System;
using System.Linq;
using System.Text;

namespace CrabSeek.Connectors.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HopefullyShouldWork()
        {
            int numberOfRooms = 5;

            var tileGenerator = new TileGenerator
            {
                UseConnectors = true,
                ConnectAllAdjacent = true,
                MaximumHeight = 4,
                MaximumWidth = 8
            };

            var tiles = tileGenerator.GenerateTiles(numberOfRooms);

            var grid = new TileGrid(tiles);
            var gridBytes = grid.ToBase2ByteGrid();
            var gridString = grid.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("\nGRID:");
            PrintGrid(gridString);

            Console.WriteLine("\nBASE2 BYTE GRID:\n");
            var byteGridString = ByteGridToString(gridBytes);
            PrintGrid(byteGridString);

            Assert.IsTrue(tiles.Count(t => t.Type == TileType.Room) == numberOfRooms);
            Assert.IsTrue(tiles.Count(t => t.Type == TileType.Connector) >= numberOfRooms - 1);
        }

        private void PrintGrid(string[] grid)
        {
            Console.WriteLine("╔" + new string('═', grid.Max(s => s.Length) + 2) + "╗");

            for (int i = 0; i < grid.Length; i++)
                Console.WriteLine($"║ {grid[i]} ║");

            Console.WriteLine("╚" + new string('═', grid.Max(s => s.Length) + 2) + "╝");
        }

        private string[] ByteGridToString(byte[,] byteGrid)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < byteGrid.GetLength(0); i++)
            {
                for (int j = 0; j < byteGrid.GetLength(0); j++)
                {
                    sb.Append(byteGrid[i, j]);
                }

                sb.Append(Environment.NewLine);
            }

            var base2GridString = sb.ToString();

            return base2GridString.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}