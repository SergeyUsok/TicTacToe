using System.Collections.Generic;
using System.IO;
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
            set
            {
                if (value == _selectedPlayerX) return;
                _selectedPlayerX = value;
                OnPropertyChanged();
            }
        }

        private int _depthX = 5; // default depth value
        private string _depthXString = 5.ToString(); // default value to show
        public string DepthX
        {
            get { return _depthXString; }
            set
            {
                if (value == _depthXString) return;

                _depthXString = value;

                int result;
                if (int.TryParse(value, out result))
                {
                    _depthX = result;
                }
                else
                {
                    _depthX = int.MinValue;
                    throw new InvalidDataException("Invalid Depth specified");
                }
                    

                OnPropertyChanged();
            }
        }

        private string _selectedPlayer0 = ResourcesHolder.HumanSymbol; // default value to show
        public string SelectedPlayer0
        {
            get { return _selectedPlayer0; }
            set
            {
                if (value == _selectedPlayer0) return;
                _selectedPlayer0 = value;
                OnPropertyChanged();
            }
        }



        private int _depthO = 5; // default depth value
        private string _depthOString = 5.ToString(); // default value to show
        public string DepthO
        {
            get { return _depthOString; }
            set
            {
                if (value == _depthOString) return;

                _depthOString = value;

                int result;
                if (int.TryParse(value, out result))
                {
                    _depthO = result;
                }
                else
                {
                    _depthO = int.MinValue;
                    throw new InvalidDataException("Invalid Depth specified");
                }

                OnPropertyChanged();
            }
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
                    _startGame = new RelayCommand(_ => NotifyGameStarted(), _ => ParametersAreValid());

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
            return PlayerViewModelsFactory.GetPlayer(selectedPlayerX, Mark.Cross, game, _depthX);
        }

        private IPlayerViewModel GetPlayer0(Game game, string selectedPlayer0)
        {
            return PlayerViewModelsFactory.GetPlayer(selectedPlayer0, Mark.Nought, game, _depthO);
        }

        private bool ParametersAreValid()
        {
            var isValid = SelectedBoard != null && SelectedPlayer0 != null && SelectedPlayerX != null;

            if (SelectedPlayerX != null &&
                (SelectedPlayerX.Equals(ResourcesHolder.MiniMaxPlayer) ||
                 SelectedPlayerX.Equals(ResourcesHolder.OptimizedMinimaxPlayer)))
            {

                isValid = isValid && _depthX >= 0;
            }

            if (SelectedPlayer0 != null &&
                (SelectedPlayer0.Equals(ResourcesHolder.MiniMaxPlayer) ||
                 SelectedPlayer0.Equals(ResourcesHolder.OptimizedMinimaxPlayer)))
            {

                isValid = isValid && _depthO >= 0;
            }

            return isValid;
        }
    }
}
