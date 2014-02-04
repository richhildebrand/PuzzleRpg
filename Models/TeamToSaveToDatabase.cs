using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class TeamToSaveToDatabase
    {
        public int TeamId { get; set; }
        public List<TeamMemberToSaveToDatabase> TeamMembers { get; set; }

        private TeamToSaveToDatabase() { } // needed to save in database
    }
}
