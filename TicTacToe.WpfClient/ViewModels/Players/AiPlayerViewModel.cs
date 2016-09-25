using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;
using TicTacToe.Core.Players;
using TicTacToe.ViewModels.Events;

namespace TicTacToe.ViewModels.Players
{
    class AiPlayerViewModel : IPlayerViewModel
    {
        private readonly AiPlayer _aiPlayer;

        public AiPlayerViewModel(AiPlayer aiPlayer)
        {
            _aiPlayer = aiPlayer;
        }

        public void MakeMove(BoardViewModel board)
        {
            board.IsActive = false; // make board inactive for user's input

            Task.Run(() => _aiPlayer.MakeMove())
                .ContinueWith(t => EventAggregator.Instance.Publish(new MoveEvent(t.Result)),
                              TaskScheduler.FromCurrentSynchronizationContext());
        }

        public Mark MyMark
        {
            get
            {
                return _aiPlayer.MyMark;
            }
        }
    }
}
