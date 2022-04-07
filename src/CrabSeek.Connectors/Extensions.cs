using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabSeek.Connectors
{
    internal static class Extensions
    {
        public static bool HasXY(this ITile tile, XY xy) => tile.X == xy.X && tile.Y == xy.Y;
    }
}
