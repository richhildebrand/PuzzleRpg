using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.Interface;
using SimpleMvvmToolkit;

namespace PuzzleRpg.Modals.EnterDungeonErrors
{
    public partial class PlayerOwnsTooManyHeroesError : UserControl, IModalControl
    {
        private readonly int _numberOfHeroesOwnedbyPlayer;

        public event EventHandler CloseModal;

        public PlayerOwnsTooManyHeroesError(int numberOfHeroesOwnedbyPlayer)
        {
            this._numberOfHeroesOwnedbyPlayer = numberOfHeroesOwnedbyPlayer;
            InitializeComponent();
            Error.Text = GetErrorText();
        }

        private string GetErrorText()
        {
            var errorMessage = "You have " + _numberOfHeroesOwnedbyPlayer + " heroes but";
            errorMessage += " only have room for " + AppSettings.MaxNumberOfHeroesPlayerCanOwn;
            errorMessage += ". Please delete some heroes before proceding your next dungeon.";
            return errorMessage;
        }
            
        public void NavigateToHeroes(object sender, GestureEventArgs e)
        {
            var url = "/HeroOptions.xaml";
            MessageBus.Default.Notify("NavigateToPage", new Object(), new NotificationEventArgs(url));
            CloseModal(new Object(), new EventArgs());
        }
    }
}
