using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TicTacToe.Annotations;
using TicTacToe.Core;
using TicTacToe.Core.DataObjects;
using TicTacToe.ViewModels.Helpers;
using TicTacToe.ViewModels.Players;

namespace TicTacToe.ViewModels
{
    // TODO: EventTrigger for animation
    // TODO: Add styles
    // TODO: highlight moves by clicking on moves history
    class GameViewModel : ViewModelBase, IDisposable
    {
        private readonly IGame _game;
        private readonly IPlayerViewModel _playerX;
        private readonly IPlayerViewModel _player0;

        public GameViewModel([NotNull] IGame game, [NotNull] IPlayerViewModel playerX,
                             [NotNull] IPlayerViewModel player0)
        {
            if (game == null) 
                throw new ArgumentNullException("game");

            if (playerX == null) 
                throw new ArgumentNullException("playerX");

            if (player0 == null) 
                throw new ArgumentNullException("player0");

            _game = game;
            _playerX = playerX;
            _player0 = player0;

            Board = new BoardViewModel(game.Board);
            Timer = new TimerViewModel();
            MessagesStack = new ObservableCollection<string>();
        }

        public async Task StartGameLoopAsync()
        {
            Timer.Start();

            var playersIterator = new RoundRobinIterator<IPlayerViewModel>(_playerX, _player0);

            MoveResult moveResult;

            do
            {
                var nextPlayer = playersIterator.Next();

                MessagesStack.Push(ResourcesHolder.GetTurnMessage(nextPlayer.MyMark));

                var move = await nextPlayer.MakeMoveAsync(Board);

                MessagesStack.Push(ResourcesHolder.GetMoveMessage(nextPlayer.MyMark));

                // set Mark to proper board cell
                Board.SetBoardCell(move.X, move.Y, move.Mark);

                moveResult = _game.GetMoveResult(move);

            } while (moveResult.GameState == GameState.KeepPlaying);

            ProcessGameOver(moveResult);
        }

        public BoardViewModel Board { get; private set; }
        public TimerViewModel Timer { get; private set; }

        public ObservableCollection<string> MessagesStack { get; set; }

        private void ProcessGameOver(MoveResult moveResult)
        {
            Board.IsActive = false;
            Timer.Stop();

            if (moveResult.GameState == GameState.Draw)
            {
                MessagesStack.Push(ResourcesHolder.GetDrawMessage());

            }
            else
            {
                MessagesStack.Push(ResourcesHolder.GetWinMessage(moveResult.Mark));
                Board.HighlightWinRow(moveResult.WinRow);
            }
        }

        public void Dispose()
        {
            _playerX.CleanUp();
            _player0.CleanUp();
        }
    }
}
