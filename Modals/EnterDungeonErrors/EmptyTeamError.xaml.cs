using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.Interface;
using SimpleMvvmToolkit;

namespace PuzzleRpg.Modals.EnterDungeonErrors
{
    public partial class EmptyTeamError : UserControl, IModalControl
    {
        public event EventHandler CloseModal;

        public EmptyTeamError()
        {
            InitializeComponent();
        }

        public void NavigateToTeamSeletion(object sender, GestureEventArgs e)
        {
            var url = "/TeamSelection.xaml";
            MessageBus.Default.Notify("NavigateToPage", new Object(), new NotificationEventArgs(url));
            CloseModal(this, new EventArgs());
        }
    }
}
