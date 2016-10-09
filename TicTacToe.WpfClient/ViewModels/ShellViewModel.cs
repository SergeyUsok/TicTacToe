using TicTacToe.ViewModels.Events;

namespace TicTacToe.ViewModels
{
    class ShellViewModel : ViewModelBase
    {
        private GameViewModel _game;

        public ShellViewModel()
        {
            GamePreferences = new GamePreferencesViewModel();
            EventAggregator.Instance.Subscribe<GameStartedEvent>(OnGameStarted);
        }

        public GameViewModel Game
        {
            get { return _game; }
            private set
            {
                if (Equals(value, _game)) return;
                _game = value;
                OnPropertyChanged();
            }
        }

        public GamePreferencesViewModel GamePreferences { get; private set; }

        private void OnGameStarted(GameStartedEvent @event)
        {
            Game = new GameViewModel(@event.Game, @event.PlayerX, @event.PlayerO);
            Game.StartGameLoop();
        }
    }
}
