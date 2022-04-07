namespace CrabSeek.Connectors
{
    public class Constants
    {
        public const int TILE_STEP = 1;
        public const int TILE_STEP_CONNECTORS = 2;

        public const int TILE_DEFAULT_X = -1;
        public const int TILE_DEFAULT_Y = -1;
        public const int TILE_DIRECTIONS = 4;

        public const int GRID_DEFAULT_WIDTH = 32;

        public const byte TILE_COST_VOID = 0;
        public const byte TILE_COST_ROOM = 2;
        public const byte TILE_COST_CONNECTOR = 1;

        public const string TILE_NAME_VOID = "#";
        public const string TILE_NAME_ROOM = "R";
        public const string TILE_NAME_CONNECTOR = "C";
    }
}