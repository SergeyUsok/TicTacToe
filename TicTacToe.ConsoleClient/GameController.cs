using System;
using TicTacToe.Core;
using TicTacToe.Core.DataObjects;
using TicTacToe.Core.Players;

namespace TicTacToe.ConsoleClient
{
    class GameController
    {
        private const int Depth = 10;
        
        public void StartNewGame()
        {
            var parameters = GetGameParameters();
            var game = CreateNewGame(parameters);
            var minimaxPlayer = GetMinimaxPlayer(game);

            StartGameLoop(game, minimaxPlayer);
        }

        #region Game loop

        private void StartGameLoop(Game game, AiPlayer minimaxAiPlayer)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"New game started. To interrupt the game type 'exit'");

            var state = State.HumanTurn;
            MoveResult result = null;

            while (state == State.AiTurn || state == State.HumanTurn)
            {
                game.Board.DrawBoard();

                var move = state == State.HumanTurn ? 
                                            HumanMakeMove(game, ref state) 
                                            : minimaxAiPlayer.MakeMove();  

                state = GetState(game, move, state, out result);
            }

            HandleGameOver(game, state, result);
        }

        private void HandleGameOver(Game game, State state, MoveResult result)
        {
            if (state == State.Exit)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@"User interrupted the game. Exiting");
                return;
            }
                
            if (state == State.Draw)
            {
                game.Board.DrawBoard(); // draw final board state
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(@"Game over. Draw");
                Console.ResetColor();
            }
                

            if (state == State.AiWon)
            {
                game.Board.DrawBoard(result.WinRow); // draw final board state
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"Game over. Computer won!");
                Console.ResetColor();
            }

            if (state == State.HumanWon)
            {
                game.Board.DrawBoard(result.WinRow); // draw final board state
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@"Game over. You won!");
                Console.ResetColor();
            }
        }

        private State GetState(Game game, Move move, State previousState, out MoveResult result)
        {
            if (previousState == State.Exit)
            {
                result = null;
                return State.Exit;
            }
                
            result = game.GetMoveResult(move);

            if (result.GameState == GameState.KeepPlaying)
            {
                return previousState == State.AiTurn ? State.HumanTurn : State.AiTurn;
            }

            if (result.GameState == GameState.Victory)
            {
                return previousState == State.AiTurn ? State.AiWon : State.HumanWon;
            }

            return State.Draw;
        }

        private Move HumanMakeMove(Game game, ref State state)
        {
            int x = -1;
            int y = -1;
            string input;

            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@"Make your move in format 'row-column'");
                Console.ForegroundColor = ConsoleColor.Yellow;
                
                input = Console.ReadLine();
                
                Console.ForegroundColor = ConsoleColor.Cyan;

                if(input.ToUpper() == "EXIT")
                    state = State.Exit;

            } while (state != State.Exit && !ParseInput(input, game, out x, out y));
            
            if(state != State.Exit)
                game.Board[x, y] = Mark.Cross;

            return new Move(x, y, Mark.Cross);
        }

        private bool ParseInput(string input, Game game, out int x, out int y)
        {
            // out values should be assigned
            x = -1;
            y = -1;
            
            var coords = input.Split('-');

            if (coords.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(@"Unrecognized format of user input");
                Console.ResetColor();
                return false;
            }

            if (int.TryParse(coords[0], out x) && int.TryParse(coords[1], out y))
            {
                x = x - 1;
                y = y - 1;

                if (game.Board.WithinBounds(x, y) && game.Board[x, y] == Mark.Empty)
                {
                    return true;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"Incorrect move. Probably chosen cell is not empty or it out of board bounds");
            Console.ResetColor();

            return false;
        }

        #endregion
        
        #region Prepration

        private GameSettings GetGameParameters()
        {
            return new GameSettings(3, 3, 3);
        }

        private AiPlayer GetMinimaxPlayer(Game game)
        {
            return new MiniMaxAiPlayer(game, Mark.Nought, Depth);
        }

        private Game CreateNewGame(GameSettings settings)
        {
            return new Game(settings);
        }

        #endregion
    }
}
