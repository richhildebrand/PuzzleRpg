using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using PuzzleRpg.Utils;

namespace PuzzleRpg.CustomControls
{
    public partial class MoveTimer : UserControl
    {
        public MoveTimer()
        {
            InitializeComponent();
        }

        private Task DrawHealthBar(double newTime)
        {
            var timerColumn = Timer.RowDefinitions[0];
            var oldMaxHeight = Convert.ToInt32(timerColumn.MaxHeight);
            var newMaxHeight = Convert.ToInt32(newTime);

            timerColumn.MaxHeight = newMaxHeight;
            return Task.WhenAll(AnimateTimer.Animate(timerColumn, oldMaxHeight, newMaxHeight));
        }
    }
}
