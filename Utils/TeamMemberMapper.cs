using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public static class TeamMemberMapper
    {
        public static List<TeamMemberToSaveToDatabase> Map(List<TeamMember> teamMembers)
        {
            var membersForDatabase = new List<TeamMemberToSaveToDatabase>();
            foreach (var member in teamMembers)
            {
                var slot = member.Slot;
                var id = member.ThisHero.Id;
                var memberForDatabase = new TeamMemberToSaveToDatabase(slot, id);
                membersForDatabase.Add(memberForDatabase);
            }

            return membersForDatabase;
        }
    }
}
