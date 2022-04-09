﻿namespace CrabSeek.Connectors
{
    internal abstract class Tile : ITile
    {
        public Tile(int x, int y, string name)
        {
            X = x;
            Y = y;
            Name = name;
            Cost = (byte)(int)Type;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public byte Cost { get; set; }
        public string Name { get; set; }
        public virtual TileType Type => TileType.Inaccessible;
    }
}