using System;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class TeamMember
    {
        public int Slot { get; set; }
        public Hero ThisHero { get; set; }

        private TeamMember() {  } //this allows object to be save in local storage

        public TeamMember(int slot, Hero hero)
        {
            Slot = slot;
            ThisHero = hero;
        }
    }
}
