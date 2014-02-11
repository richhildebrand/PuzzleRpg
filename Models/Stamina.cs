using System;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class Stamina
    {
        public int Max { get; set; }
        public int Current { get; set; }

        public Stamina() {} // to save object in local storage
    }
}
