using System;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class Node
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Node(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
