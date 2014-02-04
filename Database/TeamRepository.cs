using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;


namespace PuzzleRpg.Database
{
    public class TeamRepository : Repository
    {
        private readonly string TEAMS_KEY = "Teams";
        private HeroRepository _heroRepository;

        public TeamRepository()
        {
            _heroRepository = new HeroRepository();
            CreateKeyIfMissing(TEAMS_KEY);
        }

        public List<TeamMemberToSaveToDatabase> GetTeam()
        {
            var heroesOnTeam = IsolatedStorageSettings.ApplicationSettings[TEAMS_KEY] as List<TeamMemberToSaveToDatabase>;
            heroesOnTeam = RemoveHeroesNoLongerOwnedByPlayer(heroesOnTeam);

            return heroesOnTeam;
        }

        public void SaveTeam(List<TeamMember> teamMembers)
        {
            var membersForDatabase = TeamMemberMapper.Map(teamMembers);

            IsolatedStorageSettings.ApplicationSettings[TEAMS_KEY] = membersForDatabase;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private List<TeamMemberToSaveToDatabase> RemoveHeroesNoLongerOwnedByPlayer(List<TeamMemberToSaveToDatabase> teamMembers)
        {
            var ownedHeroes = _heroRepository.GetHeroesOwnedByPlayer();
            var ownedHeroIds = ownedHeroes.Select(h => h.Id);
            return teamMembers.Where(tm => ownedHeroIds.Contains(tm.HeroId)).ToList();
        }

        protected override void CreateKey(string key)
        {
            var teamMembers = new List<TeamMemberToSaveToDatabase>();
            IsolatedStorageSettings.ApplicationSettings.Add(TEAMS_KEY, teamMembers);
        }
    }
}
