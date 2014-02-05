using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Database;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public static class TeamMemberMapper
    {
        private static HeroRepository _heroRepository;

        static TeamMemberMapper()
        {
            _heroRepository = new HeroRepository();
        }

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

        public static List<TeamMember> Map(List<TeamMemberToSaveToDatabase> teamMembersFromDatabase)
        {
            var allHeroesPlayerOwns = _heroRepository.GetHeroesOwnedByPlayer();

            var teamMembers = new List<TeamMember>();
            foreach (var teamMemberFromDB in teamMembersFromDatabase)
            {
                var slot = teamMemberFromDB.TeamSlot;
                var hero = allHeroesPlayerOwns.Single(h => h.Id == teamMemberFromDB.HeroId);

                teamMembers.Add(new TeamMember(slot, hero));
            }

            return teamMembers;
        }
    }
}
