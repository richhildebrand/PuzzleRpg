using System;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class Stamina
    {
        public int Max { get; set; }
        public DateTime LastGainedStamina { get; set; }

        private int _current;
        public int Current
        {
            get { return _current; }
            set 
            {
                LastGainedStamina = DateTime.Now;
                value = (value > Max) ? Max : value;
                _current = value;
            }
        }

        private Stamina() {} // to save object in local storage

        public Stamina(int max)
        {
            Max = max;
            Current = max;
            LastGainedStamina = DateTime.Now;
        }
    }
}
