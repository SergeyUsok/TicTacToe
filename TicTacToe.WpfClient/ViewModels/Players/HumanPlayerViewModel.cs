using TicTacToe.Core.DataObjects;
using TicTacToe.ViewModels.Events;

namespace TicTacToe.ViewModels.Players
{
    class HumanPlayerViewModel : IPlayerViewModel
    {
        private readonly Mark _playersMark;
        private bool _isMyTurn = false; // required for case when we have 2 Human players and need to determine which one is should move

        public HumanPlayerViewModel(Mark playersMark)
        {
            _playersMark = playersMark;
            EventAggregator.Instance.Subscribe<TileClickedEvent>(OnTileClicked);
        }

        public void MakeMove(BoardViewModel board)
        {
            _isMyTurn = true;
            board.IsActive = true; // make board available for user's input
        }

        public Mark MyMark
        {
            get
            {
                return _playersMark;
            }
        }

        private void OnTileClicked(TileClickedEvent @event)
        {
            // we cannot change this variable after event raising
            // because event will begin chain reaction of game moves
            // it will be set only after chain finishes
            var isMyTurn = _isMyTurn;
            _isMyTurn = false;

            if(isMyTurn)
                EventAggregator.Instance.Publish(new MoveEvent(new Movement(@event.X, @event.Y, _playersMark)));
        }
    }
}
