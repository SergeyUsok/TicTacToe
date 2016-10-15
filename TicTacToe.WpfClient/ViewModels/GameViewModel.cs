using System;
using System.Collections.ObjectModel;
using TicTacToe.Annotations;
using TicTacToe.Core;
using TicTacToe.Core.DataObjects;
using TicTacToe.ViewModels.Helpers;
using TicTacToe.ViewModels.Players;

namespace TicTacToe.ViewModels
{
    // TODO: 4. EventTrigger for animation
    // TODO: 9. Add styles
	// TODO: Alpha-beta cutoff
    // TODO: add another AI
    class GameViewModel : ViewModelBase
    {
        private readonly IGame _game;
        private readonly RoundRobinIterator<IPlayerViewModel> _playersIterator;
        
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

            Board = new BoardViewModel(game.Board);
            Timer = new TimerViewModel();
            MessagesStack = new ObservableCollection<string>();

            _playersIterator = new RoundRobinIterator<IPlayerViewModel>(playerX, player0);
        }

        public async void StartGameLoop()
        {
            Timer.Start();

            MoveResult moveResult;

            do
            {
                var nextPlayer = _playersIterator.Next();

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
    }
}
