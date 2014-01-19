using System;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class OrbMove
    {
        public Node Origin { get; set; }
        public Node Destination { get; set; }

        public OrbMove(Node origin, Node destination)
        {
            Origin = origin;
            Destination = destination;
        }
    }
}
