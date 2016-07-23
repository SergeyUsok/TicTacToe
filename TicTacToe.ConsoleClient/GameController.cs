using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core;
using TicTacToe.Core.DataObjects;
using TicTacToe.Core.Players;

namespace TicTacToe.ConsoleClient
{
    class GameController
    {
        private bool _moveAllow = true;
        private Mark[,] _board;
        private HumanPlayer _player;
        private bool _isGameEnded = false;

        public void StartNewGame()
        {
            Console.WriteLine("New game started. To interrupt the game type 'exit'");
            Console.WriteLine("Make your move in format 'row-column'");

            _isGameEnded = false;
            _moveAllow = true;
            var parameters = GetGameParameters();
            _board = new Mark[parameters.Width, parameters.Height];
            _player = PrepareHumanPlayer(parameters);

            var game = CreateNewGame(parameters);
            game.GameEnded += OnGameEnded;

            Console.WriteLine(_board.ToStringView());

            while (!_isGameEnded)
            {
                var read = Console.ReadLine();

                if (read == "exit")
                    break;

                ProcessInput(read);
            }
        }

        private void OnGameEnded(object sender, GameEndedEventArgs args)
        {
            _isGameEnded = true;

            if (args.Result == MoveResult.Draw)
            {
                Console.WriteLine("Game ended with draw");
            }
            else
            {
                if(args.Winner == _player)
                    Console.WriteLine("You win!");
                else
                    Console.WriteLine("You lose");
            }
        }

        private void ProcessInput(string read)
        {
            if (_moveAllow)
            {
                ParseAndMove(read);
            }
            else
            {
                Console.WriteLine("Please wait until AI make move");
            }
        }

        private void ParseAndMove(string read)
        {
            var coords = read.Split('-');

            if (coords.Length != 2)
            {
                Console.WriteLine("Unrecognized format of user input. Please pass your move in format 'row-column'");
                return;
            }

            int x;
            int y;
            if (int.TryParse(coords[0], out x) && int.TryParse(coords[1], out y))
            {
                x = x - 1;
                y = y - 1;

                if (WithinBounds(x, y) && _board[x, y] == Mark.Empty)
                {
                    _board[x, y] = Mark.Cross;
                    Console.WriteLine(_board.ToStringView());
                    _moveAllow = false;
                    _player.MakeMove(new Movement(x, y, Mark.Cross));
                }
                else
                {
                    Console.WriteLine("Incorrect move. Probably chosen cell not empty or it out of board bounds");
                }
            }
            else
            {
                Console.WriteLine("Unrecognized format of user input. Please pass your move in format 'row-column'");
            }
        }

        private bool WithinBounds(int x, int y)
        {
            return x >= 0 &&
                   x < _board.GetLength(0) &&
                   y >= 0 &&
                   y < _board.GetLength(1);
        }

        private Game CreateNewGame(GameParameters parameters)
        {
            return new Game(_player, 
                            new MiniMaxPlayer(parameters, Mark.Zero, 10), 
                            parameters);
        }

        private GameParameters GetGameParameters()
        {
            return new GameParameters(3, 3, 3);
        }

        private HumanPlayer PrepareHumanPlayer(GameParameters parameters)
        {
            var player = new HumanPlayer(parameters, Mark.Cross);

            player.AllowMovement += OnMoveAllowment;
            return player;
        }

        private void OnMoveAllowment(object sender, BoardChangedEventArgs args)
        {
            _moveAllow = true;
            Console.WriteLine(args.Board.ToStringView());
            Console.WriteLine("Make your move in format 'row-column'");
        }
    }
}
