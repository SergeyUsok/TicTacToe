using System;
using System.Threading;
using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;
using TicTacToe.Core.Players;
using TicTacToe.ViewModels.Events;

namespace TicTacToe.ViewModels.Players
{
    class AiPlayerViewModel : IPlayerViewModel
    {
        private readonly AiPlayer _aiPlayer;
        private readonly CancellationTokenSource _cancelationSource = new CancellationTokenSource();

        public AiPlayerViewModel(AiPlayer aiPlayer)
        {
            _aiPlayer = aiPlayer;
        }

        public async Task<Move> MakeMoveAsync(BoardViewModel board)
        {
            board.IsActive = false; // make board inactive for user's input

            var token = _cancelationSource.Token;

            return await Task.Run(() =>
                                {
                                    var task = Task.Run(() => _aiPlayer.MakeMove(), token);

                                    var tcs = new TaskCompletionSource<Move>();

                                    while(task.Status != TaskStatus.RanToCompletion && task.Status != TaskStatus.Faulted)
                                    {
                                        Thread.Sleep(100);
                                        token.ThrowIfCancellationRequested();
                                    }

                                    return task.Result;
                                });
        }

        public void CleanUp()
        {
            // cancel any long running AI player
            _cancelationSource.Cancel();
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
