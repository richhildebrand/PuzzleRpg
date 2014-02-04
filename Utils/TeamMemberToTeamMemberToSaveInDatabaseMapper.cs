using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public static class TeamMemberToTeamMemberToSaveInDatabaseMapper
    {
        public static List<TeamMemberToSaveToDatabase> Map(List<TeamMember> teamMembers) 
        {
            var membersForDatabase = new List<TeamMemberToSaveToDatabase>();
            foreach (var teamMember in teamMembers)
            {
                var id = teamMember.ThisHero.Id;
                var slot = teamMember.Slot;

                membersForDatabase.Add(new TeamMemberToSaveToDatabase(slot, id));
            }

            return membersForDatabase;
        }
    }
}
