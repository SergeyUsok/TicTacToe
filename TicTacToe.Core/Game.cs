using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;
using TicTacToe.Core.Players;

namespace TicTacToe.Core
{
    // TODO 1) Game.StartNew() 2) Board as propery 3) Notification board changed
    public sealed class Game
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly Mark[,] _board;
        private readonly GameParameters _parameters;

        public Game(Player player1, Player player2, GameParameters parameters)
        {
            _player1 = player1;
            _player2 = player2;

            _player1.Move += HandleMove;
            _player2.Move += HandleMove;

            _board = new Mark[parameters.Width,parameters.Height];

            _parameters = parameters;
        }

        public event EventHandler<GameEndedEventArgs> GameEnded;

        private void OnGameEnded(GameEndedEventArgs e)
        {
            var handler = GameEnded;
            if (handler != null) handler(this, e);
        }

        // TODO handle Movement and GameStatus here and Raise event about that
        private void HandleMove(object sender, MoveEventArgs args)
        {
            var player = (Player)sender;
            var nextPlayer = player == _player1 ? _player2 : _player1;
            
            _board[args.Movement.X, args.Movement.Y] = args.Movement.Mark;

            var moveResult = GameStateChecker.GetMoveResult(_board, args.Movement, _parameters.NumberInRowToWin);

            if (moveResult == MoveResult.KeepPlaying)
                nextPlayer.MakeMove(_board);
            else
                OnGameEnded(new GameEndedEventArgs(moveResult, moveResult == MoveResult.Victory ? player : null));
        }
    }
}
