using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class TeamSelection : PhoneApplicationPage
    {
        public TeamSelection()
        {
            InitializeComponent();
            ShowTeam();
        }

        public void ChangeTeamMember(object sender, GestureEventArgs e)
        {
            var x = 3;
            var y = x;
        }

        private void ShowTeam()
        {
            Hero[] activeTeam = new Hero[AppGlobals.MaxHeroesOnATeam];
            Team.ItemsSource = HeroToViewModelMapper.GetHeroViewModels(activeTeam);
        }
    }
}