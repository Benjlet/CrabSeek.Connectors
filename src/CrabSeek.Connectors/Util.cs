using System.Numerics;

namespace CrabSeek.Connectors
{
    internal static class Util
    {
        private static readonly Random _random = new();

        internal static int GetRandom() => _random.Next();

        internal static int GetRandomEvenNumber(int maximum) => 2 * _random.Next(maximum / 2);

        internal static bool GreaterThanZero(params int[] values) => values.All(v => v > 0);
        
        internal static uint GetBase2GridSize(int width, int height) => BitOperations.RoundUpToPowerOf2((uint)(width > height ? width : height));
    }
}
