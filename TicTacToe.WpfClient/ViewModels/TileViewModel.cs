using System;
using System.Windows.Media;
using TicTacToe.Core.DataObjects;
using TicTacToe.ViewModels.Events;
using TicTacToe.ViewModels.Helpers;

namespace TicTacToe.ViewModels
{
    class TileViewModel : ViewModelBase
    {
        private Mark _value;
        private string _markView = ResourcesHolder.Void;
        private RelayCommand _relayCommand;
        private bool _isWinTile;
        private bool _isActive = true;

        public TileViewModel(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (value.Equals(_isActive)) return;
                _isActive = value;
                OnPropertyChanged();
                OnPropertyChanged("MoveCommand");
            }
        }

        public bool IsWinTile
        {
            get { return _isWinTile; }
            set
            {
                if (value.Equals(_isWinTile)) return;
                _isWinTile = value;
                OnPropertyChanged();
            }
        }

        public Mark Value
        {
            get { return _value; }
            set
            {
                if (value == _value)
                    return;
                
                _value = value;
                IsActive = false;
                UpdateMarkView();
            }
        }

        private void UpdateMarkView()
        {
            switch (Value)
            {
                case Mark.Cross:
                    MarkView = ResourcesHolder.Cross;
                    break;
                case Mark.Nought:
                    MarkView = ResourcesHolder.Nought;
                    break;
                case Mark.Empty: // this implemented for undo possibilities
                    MarkView = ResourcesHolder.Void;
                    break;
            }
        }

        public string MarkView
        {
            get { return _markView; }
            set
            {
                if (_markView.Equals(value)) 
                    return;
                
                _markView = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand MoveCommand
        {
            get
            {
                if(_relayCommand == null)
                    _relayCommand = new RelayCommand(_ => NotifyAboutMove(), _ => IsActive);

                return _relayCommand;
            }
        }

        private void NotifyAboutMove()
        {
            EventAggregator.Instance.Publish(new TileClickedEvent(X, Y));
        }
    }
}
