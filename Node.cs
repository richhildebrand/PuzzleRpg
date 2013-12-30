using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleRpg
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
