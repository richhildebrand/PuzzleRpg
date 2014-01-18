using System;
using System.Linq;

namespace PuzzleRpg
{
    public class OrbMatch
    {
        public AppGlobals.Types Type { get; set; }
        public int Count { get; set; }
        public bool HasHorizontalMatch { get; set; }

        public OrbMatch(AppGlobals.Types type, int count, bool hasHorizontalMatch)
        {
            Type = type;
            Count = count;
            HasHorizontalMatch = hasHorizontalMatch;
        }
    }
}
