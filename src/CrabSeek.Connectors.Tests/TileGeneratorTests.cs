using NUnit.Framework;
using System;
using System.Linq;

namespace CrabSeek.Connectors.Tests
{
    public class TileGeneratorTests
    {
        [Test]
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(8)]
        public void GivenNumberOfRooms_ShouldGenerateThatManyRooms(int numberOfRooms)
        {
            var tileGenerator = new TileGenerator
            {
                MaximumHeight = 4,
                MaximumWidth = 8
            };

            var tiles = tileGenerator.GenerateTiles(numberOfRooms);

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsTrue(tiles.Count(t => t.Type == TileType.Room) == numberOfRooms);
        }

        [Test]
        public void GivenRoomNames_ShouldGenerateThoseRooms()
        {
            var tileGenerator = new TileGenerator
            {
                MaximumHeight = 4,
                MaximumWidth = 8,
                UseConnectors = false
            };

            var roomNames = new string[]
            {
                "OBSERVATORY",
                "GRANARY",
                "CHAPEL",
                "SHED"
            };

            var tiles = tileGenerator.GenerateTiles(roomNames);

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsTrue(tiles.Count() == roomNames.Length);
            Assert.IsTrue(tiles.All(t => roomNames.Contains(t.Name)));
        }

        [Test]
        public void GivenNoRoomNames_ShouldGenerateNoRooms()
        {
            var tileGenerator = new TileGenerator
            {
                MaximumHeight = 4,
                MaximumWidth = 8
            };

            var tiles = tileGenerator.GenerateTiles(Enumerable.Empty<string>());

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsFalse(tiles.Any());
        }

        [Test]
        [TestCase(3)]
        [TestCase(8)]
        public void GivenMaximumHeight_ShouldGenerateWithinHeight(int height)
        {
            int totalRooms = 10;

            var tileGenerator = new TileGenerator
            {
                MaximumHeight = height,
                UseConnectors = false
            };

            var tiles = tileGenerator.GenerateTiles(totalRooms);

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsTrue(tiles.Count() == totalRooms);
            Assert.IsTrue(tiles.All(t => t.Y <= height));
            Assert.IsTrue(tiles.All(t => t.Type == TileType.Room));
        }

        [Test]
        [TestCase(3)]
        [TestCase(8)]
        public void GivenMaximumWidth_ShouldGenerateWithinWidth(int width)
        {
            int totalRooms = 10;

            var tileGenerator = new TileGenerator
            {
                MaximumWidth = width,
                UseConnectors = false
            };

            var tiles = tileGenerator.GenerateTiles(totalRooms);

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsTrue(tiles.Count() == totalRooms);
            Assert.IsTrue(tiles.All(t => t.X <= width));
            Assert.IsTrue(tiles.All(t => t.Type == TileType.Room));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        public void GivenInvalidWidth_ShouldReturnNoTiles(int width)
        {
            int totalRooms = 10;

            var tileGenerator = new TileGenerator
            {
                MaximumWidth = width,
                UseConnectors = false
            };

            var tiles = tileGenerator.GenerateTiles(totalRooms);

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsFalse(tiles.Any());
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        public void GivenInvalidHeight_ShouldReturnNoTiles(int height)
        {
            int totalRooms = 10;

            var tileGenerator = new TileGenerator
            {
                MaximumHeight = height,
                UseConnectors = false
            };

            var tiles = tileGenerator.GenerateTiles(totalRooms);

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsFalse(tiles.Any());
        }

        [Test]
        public void GivenConnectorsDisabled_ShouldReturnRoomsWithoutConnectors()
        {
            int totalRooms = 10;

            var tileGenerator = new TileGenerator
            {
                MaximumHeight = 8,
                MaximumWidth = 8,
                UseConnectors = false
            };

            var tiles = tileGenerator.GenerateTiles(totalRooms);

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsTrue(tiles.Count() == totalRooms);
            Assert.IsTrue(tiles.All(t => t.Type == TileType.Room));
        }

        [Test]
        public void GivenConnectorsEnabled_ShouldReturnRoomsAndConnectors()
        {
            int totalRooms = 10;

            var tileGenerator = new TileGenerator
            {
                MaximumHeight = 8,
                MaximumWidth = 8,
                UseConnectors = true
            };

            var tiles = tileGenerator.GenerateTiles(totalRooms);

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsTrue(tiles.Count(t => t.Type == TileType.Room) == totalRooms);
            Assert.IsTrue(tiles.Count(t => t.Type == TileType.Connector) >= totalRooms - 1);
        }

        [Test]
        public void GivenConnectAllAdjacentEnabled_ShouldConnectAllRooms()
        {
            int totalRooms = 10;

            var tileGenerator = new TileGenerator
            {
                MaximumHeight = 8,
                MaximumWidth = 8,
                UseConnectors = true,
                ConnectAllAdjacent = false
            };

            var tiles = tileGenerator.GenerateTiles(totalRooms);

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsTrue(tiles.Count(t => t.Type == TileType.Room) == totalRooms);
            Assert.IsTrue(tiles.Count(t => t.Type == TileType.Connector) == totalRooms - 1);
        }

        [Test]
        public void GivenConnectAllAdjacentDisabled_ShouldConnectAllRooms()
        {
            int totalRooms = 10;

            var tileGenerator = new TileGenerator
            {
                MaximumHeight = 8,
                MaximumWidth = 8,
                UseConnectors = true,
                ConnectAllAdjacent = true
            };

            var tiles = tileGenerator.GenerateTiles(totalRooms);

            TestHelper.PrintGrid(new TileGrid(tiles));
            Assert.IsTrue(tiles.Count(t => t.Type == TileType.Room) == totalRooms);
            Assert.IsTrue(tiles.Count(t => t.Type == TileType.Connector) >= totalRooms - 1);
        }
    }
}