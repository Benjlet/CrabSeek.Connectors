namespace CrabSeek.Connectors
{
    /// <summary>
    /// Generates
    /// </summary>
    public class TileGenerator
    {
        private List<ITile> _tiles;
        private List<ITile> _connectionTiles;

        /// <summary>
        /// Initialises a new configurable TileGenerator, used to generate tiles and their positions.
        /// </summary>
        public TileGenerator()
        {
            _tiles = new List<ITile>();
            _connectionTiles = new List<ITile>();
        }

        /// <summary>
        /// The maximum height of the room grid. 32 by default.
        /// </summary>
        public int MaximumHeight { get; set; } = 32;

        /// <summary>
        /// The maximum width of the room grid. 32 by default.
        /// </summary>
        public int MaximumWidth { get; set; } = 32;

        /// <summary>
        /// If set, single-tile connections are generated between rooms.
        /// Otherwise all rooms will be generated directly adjacent to each other. True by default.
        /// </summary>
        public bool UseConnectors { get; set; } = true;

        /// <summary>
        /// If set, all generated rooms adjacent to another room will be connected.
        /// Lessens the appearance of dead-end corridors. True by default.
        /// </summary>
        public bool ConnectAllAdjacent { get; set; } = true;

        /// <summary>
        /// Generates pseudo-random room tile positions with the supplied room names.
        /// Optionally includes connection tiles between rooms, if UseConnectors is set.
        /// </summary>
        /// <param name="roomNames">The name of the rooms to generate tiles for.</param>
        /// <returns>A collection of tiles.</returns>
        public IEnumerable<ITile> GenerateTiles(IEnumerable<string> roomNames)
        {
            var roomTiles = GenerateTiles(roomNames.Count());
            var roomNameQueue = new Queue<string>(roomNames);

            foreach (var tile in roomTiles)
            {
                if (tile.Type == TileType.Room)
                {
                    var roomName = roomNameQueue.Dequeue();
                    tile.Name = roomName ?? string.Empty;
                }
            }

            return roomTiles;
        }

        /// <summary>
        /// Generates pseudo-random room tile positions with the supplied room names.
        /// Optionally includes connection tiles between rooms, if UseConnectors is set.
        /// </summary>
        /// <param name="roomsToCreate">The number of rooms to generate tiles for.</param>
        /// <returns>A collection of tiles.</returns>
        public IEnumerable<ITile> GenerateTiles(int roomsToCreate)
        {
            _tiles = new List<ITile>();
            _connectionTiles = new List<ITile>();

            if (GreaterThanZero(roomsToCreate, MaximumHeight, MaximumWidth))
            {
                _tiles.Add(new TileRoom(
                    Util.GetRandomEvenNumber(MaximumWidth),
                    Util.GetRandomEvenNumber(MaximumHeight)));
            }
            else
            {
                return _tiles;
            }

            int createdRooms = _tiles.Count;

            while (createdRooms < roomsToCreate)
            {
                var availableTiles = _tiles
                    .Where(t => GetAvailableAdjacentTiles(t)?.Count() > 0)?
                    .OrderBy(a => Util.GetRandom());

                bool created = availableTiles.Any(t => CreateTile(t));

                if (!created)
                    break;

                createdRooms++;
            }

            if (UseConnectors && ConnectAllAdjacent)
            {
                foreach (var tile in _tiles)
                {
                    var additional = GetAdditionalConnections(tile);
                    _connectionTiles.AddRange(additional);
                }
            }

            _tiles.AddRange(_connectionTiles);

            return _tiles;
        }

        private IEnumerable<ITile> GetAdditionalConnections(ITile tile)
        {
            var adjacentRooms = GetTakenAdjacentTiles(tile)?.OrderBy(a => Util.GetRandom())?.ToList();

            if (adjacentRooms == null || adjacentRooms.Count <= 1)
                yield break;

            for (int i = 0; i < adjacentRooms.Count; i++)
            {
                var corridorRoom = GetConnectorBetween(tile, adjacentRooms[i]);

                bool hasThisConnectionAlready = _connectionTiles.Any(c => c.HasXY(corridorRoom));

                if (!hasThisConnectionAlready)
                    yield return new TileConnector(corridorRoom.X, corridorRoom.Y);
            }
        }

        private bool CreateTile(ITile tile)
        {
            var positionOffsets = GetPositionOffsets().OrderBy(p => Util.GetRandom());

            foreach (var offset in positionOffsets)
            {
                var tileX = tile.X + offset.X;
                var tileY = tile.Y + offset.Y;

                if (IsValidGridPosition(tileX, tileY))
                    continue;

                var found = _tiles.Any(t => t.X == tileX && t.Y == tileY);

                if (!found)
                {
                    _tiles.Add(new TileRoom(tileX, tileY));

                    var corridorRoom = GetConnectorBetween(tile, new XY(tileX, tileY));

                    if (UseConnectors)
                        _connectionTiles.Add(new TileConnector(corridorRoom.X, corridorRoom.Y));

                    return true;
                }
            }

            return false;
        }

        private XY GetConnectorBetween(ITile tile, XY end)
        {
            if (end.X == tile.X)
            {
                return end.Y > tile.Y
                    ? new XY(tile.X, tile.Y + Constants.TILE_STEP)
                    : new XY(tile.X, tile.Y - Constants.TILE_STEP);
            }

            return end.X > tile.X
                ? new XY(tile.X + Constants.TILE_STEP, tile.Y)
                : new XY(tile.X - Constants.TILE_STEP, tile.Y);
        }

        private IEnumerable<XY> GetAdjacentTiles(ITile tile)
        {
            var posOffset = GetPositionOffsets();

            for (int i = 0; i < posOffset.Length; i++)
            {
                var tileX = tile.X + posOffset[i].X;
                var tileY = tile.Y + posOffset[i].Y;

                if (IsValidGridPosition(tileX, tileY))
                    continue;

                yield return new XY(tileX, tileY);
            }
        }

        private XY[] GetPositionOffsets() => new XY[]
        {
            new XY(UseConnectors ? Constants.TILE_STEP_CONNECTORS : Constants.TILE_STEP, 0 ),
            new XY(UseConnectors ? -Constants.TILE_STEP_CONNECTORS : -Constants.TILE_STEP, 0 ),
            new XY(0, UseConnectors ? -Constants.TILE_STEP_CONNECTORS : -Constants.TILE_STEP),
            new XY(0, UseConnectors ? Constants.TILE_STEP_CONNECTORS : Constants.TILE_STEP)
        };

        private IEnumerable<XY> GetTakenAdjacentTiles(ITile tile)
        {
            var adjacentTiles = GetAdjacentTiles(tile);
            return adjacentTiles.Where(a => _tiles.Any(t => t.HasXY(a)));
        }

        private IEnumerable<XY> GetAvailableAdjacentTiles(ITile tile)
        {
            var adjacentTiles = GetAdjacentTiles(tile);
            return adjacentTiles.Where(a => !_tiles.Any(t => t.HasXY(a)));
        }

        private bool GreaterThanZero(params int[] values) => values.All(v => v > 0);
        private bool IsValidGridPosition(int x, int y) => x < 0 || y < 0 || x >= MaximumWidth || y >= MaximumHeight;
    }
}