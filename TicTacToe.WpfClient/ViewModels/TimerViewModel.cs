using System;
using System.Windows.Threading;

namespace TicTacToe.ViewModels
{
    class TimerViewModel : ViewModelBase
    {
        private TimeSpan _ticks;
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public void Start()
        {
            _timer.Tick += TimerOnTick;
            _timer.Interval = TimeSpan.FromSeconds(1);

            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _timer.Tick -= TimerOnTick;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            Ticks = Ticks.Add(TimeSpan.FromSeconds(1));
        }

        public TimeSpan Ticks
        {
            get { return _ticks; }
            set
            {
                if (value.Equals(_ticks)) return;
                _ticks = value;
                OnPropertyChanged();
            }
        }
    }
}
