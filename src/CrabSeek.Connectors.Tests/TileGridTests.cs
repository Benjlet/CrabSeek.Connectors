using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CrabSeek.Connectors.Tests
{
    public class TileGridTests
    {
        [Test]
        public void GivenEmptyGrid_ToString_ShouldBeEmpty()
        {
            var tiles = Enumerable.Empty<ITile>();
            var grid = new TileGrid(tiles);
            var gridString = grid.ToString();

            TestHelper.PrintGrid(grid);
            Assert.IsTrue(grid.Width == 0);
            Assert.IsTrue(grid.Height == 0);
            Assert.IsNull(grid.Tiles);
            Assert.IsTrue(string.IsNullOrWhiteSpace(gridString));
        }

        [Test]
        public void GivenNullGrid_ToString_ShouldBeEmpty()
        {
            var grid = new TileGrid(null);
            var gridString = grid.ToString();

            TestHelper.PrintGrid(grid);
            Assert.IsTrue(grid.Width == 0);
            Assert.IsTrue(grid.Height == 0);
            Assert.IsNull(grid.Tiles);
            Assert.IsTrue(string.IsNullOrWhiteSpace(gridString));
        }

        [Test]
        public void GivenGridCollection_ToString_ShouldReturnRooms()
        {
            var tiles = new List<ITile>()
            {
                new TestTile(0, 3, "ROOM-1", 4),
                new TestTile(1, 7, "ROOM-2", 4),
                new TestTile(2, 10, "ROOM-3", 4)
            };

            var grid = new TileGrid(tiles);
            var gridString = grid.ToString();

            TestHelper.PrintGrid(grid);
            Assert.IsTrue(gridString.Count(c => c.ToString() == "R") == tiles.Count);
        }

        [Test]
        public void GivenNullTiles_ToByteGrid_ShouldBeEmpty()
        {
            var grid = new TileGrid(null);
            var gridBytes = grid.ToBase2ByteGrid();

            TestHelper.PrintGrid(grid);

            Assert.IsEmpty(gridBytes);
        }

        [Test]
        public void GivenNoTiles_ToByteGrid_ShouldBeEmpty()
        {
            var grid = new TileGrid(Enumerable.Empty<ITile>());
            var gridBytes = grid.ToBase2ByteGrid();

            TestHelper.PrintGrid(grid);

            Assert.IsEmpty(gridBytes);
        }

        [Test]
        public void GivenGridCollection_ToByteGrid_ShouldUseBase2Size()
        {
            var tiles = new List<ITile>()
            {
                new TestTile(0, 1, "ROOM-1", 4),
                new TestTile(0, 2, "ROOM-2", 4),
                new TestTile(1, 2, "ROOM-3", 4),
                new TestTile(2, 2, "ROOM-4", 4),
                new TestTile(3, 2, "ROOM-5", 4),
                new TestTile(3, 3, "ROOM-6", 4),
            };

            var grid = new TileGrid(tiles);
            var gridBytes = grid.ToBase2ByteGrid();

            var gridWidth = gridBytes.GetLength(0);
            var gridHeight = gridBytes.GetLength(1);
            var expectedSize = 4;

            TestHelper.PrintGrid(grid);
            Assert.IsTrue(gridWidth == expectedSize);
            Assert.IsTrue(gridHeight == expectedSize);
        }

        [Test]
        public void GivenGridCollection_ToByteGrid_ShouldCentreTiles()
        {
            var tiles = new List<ITile>()
            {
                new TestTile(1, 0, "ROOM-1", 4),
                new TestTile(2, 0, "ROOM-2", 4),
                new TestTile(3, 0, "ROOM-3", 4),
                new TestTile(4, 0, "ROOM-4", 4),
                new TestTile(4, 1, "ROOM-5", 4),
                new TestTile(4, 2, "ROOM-6", 4),
                new TestTile(5, 0, "ROOM-7", 4),
                new TestTile(6, 0, "ROOM-8", 4),
                new TestTile(7, 0, "ROOM-9", 4)
            };

            var grid = new TileGrid(tiles);
            var gridBytes = grid.ToBase2ByteGrid();

            var gridWidth = gridBytes.GetLength(0);
            var gridHeight = gridBytes.GetLength(1);
            var expectedSize = 8;

            TestHelper.PrintGrid(grid);
            Assert.IsTrue(gridWidth == expectedSize);
            Assert.IsTrue(gridHeight == expectedSize);
        }

        [Test]
        public void GivenGridCollection_IfNotCentreAlign_ShouldNotCentreAlign()
        {
            var tiles = new List<ITile>()
            {
                new TestTile(1, 0, "ROOM-1", 4),
                new TestTile(2, 0, "ROOM-2", 4),
                new TestTile(3, 0, "ROOM-3", 4),
                new TestTile(4, 0, "ROOM-4", 4),
            };

            var grid = new TileGrid(tiles, false);

            TestHelper.PrintGrid(grid);
        }
    }
}
