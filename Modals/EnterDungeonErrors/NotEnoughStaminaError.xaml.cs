using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.Interface;

namespace PuzzleRpg.Modals.EnterDungeonErrors
{
    public partial class NotEnoughStaminaError : UserControl, IModalControl
    {
        public event EventHandler CloseModal;

        public NotEnoughStaminaError()
        {
            InitializeComponent();
            Error.Text = GetErrorText();
        }

        private string GetErrorText()
        {
            var errorMessage = "Sorry, you do not have enough stamina to enter the dungeon.";
            errorMessage += " You will gain " + AppSettings.AmountOfStaminaToAddInterval;
            errorMessage += " stamina every " + AppSettings.GainStaminaIntervalLength.TotalMinutes;
            errorMessage += " minutes";
            return errorMessage;
        }

        public void Close(object sender, GestureEventArgs e)
        {
            CloseModal(new Object(), new EventArgs());
        }
    }
}
