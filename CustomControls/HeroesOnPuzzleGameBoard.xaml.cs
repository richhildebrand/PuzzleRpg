using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.Logic;
using PuzzleRpg.Models;

namespace PuzzleRpg.CustomControls
{
    public partial class HeroesOnPuzzleGameBoard : UserControl
    {
        private Team _activeTeam;

        public HeroesOnPuzzleGameBoard()
        {
            InitializeComponent();
        }

        public void AddHeroes(Team activeTeam)
        {
            this._activeTeam = activeTeam;
            foreach (var teamMember in activeTeam.TeamMembers)
            {
                AddTeamMember(teamMember, LayoutRoot);
            }
        }

        private void AddTeamMember(TeamMember teamMemeber, Grid grid)
        {
                var heroProfile = new HeroProfile();
                heroProfile.DrawHeroProfile(teamMemeber.ThisHero);
                grid.Children.Add(heroProfile);
                heroProfile.SetValue(Grid.RowProperty, 0);
                heroProfile.SetValue(Grid.ColumnProperty, teamMemeber.Slot*2);
                heroProfile.Tap += OnSelectHero;
        }

        private void OnSelectHero(object sender, GestureEventArgs e)
        {
            var selectedHeroProfile = sender as HeroProfile;
            var castSpellModal = new CastSpellModal(selectedHeroProfile.ThisHero);
            castSpellModal.Show();
        }
    }
}
