using NUnit.Framework;
using System;
using System.Collections;
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

            Assert.IsTrue(grid.Width == 0);
            Assert.IsTrue(grid.Height == 0);
            Assert.IsNull(grid.Tiles);
            Assert.IsTrue(string.IsNullOrWhiteSpace(gridString));
        }

        [Test]
        public void GivenGridCollection_ToString_ShouldReturnRooms()
        {
            var tiles = new List<TileRoom>()
            {
                new TileRoom(0, 3, "ROOM-1") { Cost = 4 },
                new TileRoom(1, 7, "ROOM-2") { Cost = 4 },
                new TileRoom(2, 10, "ROOM-3") { Cost = 4 }
            };

            var grid = new TileGrid(tiles);
            var gridString = grid.ToString();

            Assert.IsTrue(gridString.Count(c => c.ToString() == "R") == 3);
        }
    }
}
