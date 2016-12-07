using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;
using TicTacToe.ViewModels.Events;

namespace TicTacToe.ViewModels.Players
{
    class HumanPlayerViewModel : IPlayerViewModel
    {
        private readonly Mark _playersMark;
        private TaskCompletionSource<Move> _tcs;

        public HumanPlayerViewModel(Mark playersMark)
        {
            _playersMark = playersMark;
            EventAggregator.Instance.Subscribe<TileClickedEvent>(OnTileClicked);
        }

        public async Task<Move> MakeMoveAsync(BoardViewModel board)
        {
            board.IsActive = true; // make board available for user's input
            
            _tcs = new TaskCompletionSource<Move>();
            var task = await _tcs.Task;

            _tcs = null; // reset Task Completion Source for next move
            return task;
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
            // If task complition source is not intialized
            // then this event is not intended ofr current Player and we do nothing
            if(_tcs == null)
                return;

            _tcs.SetResult(new Move(@event.X, @event.Y, _playersMark));
        }
    }
}
