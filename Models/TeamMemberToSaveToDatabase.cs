using System;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class TeamMemberToSaveToDatabase
    {
        public int TeamSlot { get; set; }
        public Guid HeroId { get; set; }

        public TeamMemberToSaveToDatabase(int slot, Guid id)
        {
            TeamSlot = slot;
            HeroId = id;
        }

        public TeamMemberToSaveToDatabase() { }// needed to save in database
    }
}
