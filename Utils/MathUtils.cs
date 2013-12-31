using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleRpg.Utils
{
    public static class MathUtils
    {
        private static Random randomNumberGenerator = new Random();

        public static int GetRandomInteger(int min, int max)
        {
            return randomNumberGenerator.Next(min, max);
        }
    }
}
