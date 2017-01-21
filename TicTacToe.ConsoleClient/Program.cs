using System;

namespace TicTacToe.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Tic Tac Toe game");

            var gameController = new GameController();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Would you like to play new game? y/n");
                var key = Console.ReadKey();
                Console.WriteLine();

                if(key.Key == ConsoleKey.N)
                    break;
                
                if(key.Key == ConsoleKey.Y)
                    gameController.StartNewGame();
            }
        }
    }
}
