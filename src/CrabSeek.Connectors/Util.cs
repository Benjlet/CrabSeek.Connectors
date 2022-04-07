﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrabSeek.Connectors
{
    internal static class Util
    {
        private static readonly Random _random = new();

        public static int GetRandomEvenNumber(int maximum)
        {
            return 2 * _random.Next(maximum / 2);
        }

        public static int GetRandom()
        {
            return _random.Next();
        }
    }
}