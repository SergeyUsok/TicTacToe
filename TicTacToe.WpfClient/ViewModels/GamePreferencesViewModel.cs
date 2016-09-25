using System.Collections.Generic;
using System.Text.RegularExpressions;
using TicTacToe.Core;
using TicTacToe.Core.DataObjects;
using TicTacToe.ViewModels.Events;
using TicTacToe.ViewModels.Helpers;
using TicTacToe.ViewModels.Players;

namespace TicTacToe.ViewModels
{
    class GamePreferencesViewModel : ViewModelBase
    {
        public GamePreferencesViewModel()
        {
            Players = ResourcesHolder.Players;

            BoardSizes = ResourcesHolder.Boards;
        }

        #region Binded Properies

        public IReadOnlyCollection<string> Players { get; private set; }

        public IReadOnlyCollection<string> BoardSizes { get; private set; }

        private string _selectedPlayerX = ResourcesHolder.HumanSymbol; // default value to show
        public string SelectedPlayerX
        {
            get { return _selectedPlayerX; }
            set { _selectedPlayerX = value; }
        }

        private string _selectedPlayer0 = ResourcesHolder.HumanSymbol; // default value to show
        public string SelectedPlayer0
        {
            get { return _selectedPlayer0; }
            set { _selectedPlayer0 = value; }
        }

        private string _selectedBoard = "3 X 3 (3)"; // default value to show
        public string SelectedBoard
        {
            get { return _selectedBoard; }
            set { _selectedBoard = value; }
        }

        #endregion

        private RelayCommand _startGame;
        
        public RelayCommand StartGame
        {
            get
            {
                if (_startGame == null)
                    _startGame = new RelayCommand(_ => NotifyGameStarted(), _ => ParametersAreNotNull());

                return _startGame;
            }
        }

        private void NotifyGameStarted()
        {
            var game = CreateGame(SelectedBoard);
            var playerX = GetPlayerX(game, SelectedPlayerX);
            var player0 = GetPlayer0(game, SelectedPlayer0);

            EventAggregator.Instance.Publish(new GameStartedEvent(game, playerX, player0));
        }

        private Game CreateGame(string selectedBoard)
        {
            var settings = ParseGameSettings(selectedBoard);
            return new Game(settings);
        }

        private GameSettings ParseGameSettings(string selectedBoard)
        {
            var groups = Regex.Match(selectedBoard, @"(?<width>\d+) X (?<height>\d+) \((?<inRow>\d+)\)").Groups;

            var width = int.Parse(groups["width"].Value);
            var height = int.Parse(groups["height"].Value);
            var inRow = int.Parse(groups["inRow"].Value);

            return new GameSettings(width, height, inRow);
        }

        private IPlayerViewModel GetPlayerX(Game game, string selectedPlayerX)
        {
            return PlayerViewModelsFactory.GetPlayer(selectedPlayerX, Mark.Cross, game);
        }

        private IPlayerViewModel GetPlayer0(Game game, string selectedPlayer0)
        {
            return PlayerViewModelsFactory.GetPlayer(selectedPlayer0, Mark.Nought, game);
        }

        private bool ParametersAreNotNull()
        {
            return SelectedBoard != null && SelectedPlayer0 != null && SelectedPlayerX != null;
        }
    }
}
