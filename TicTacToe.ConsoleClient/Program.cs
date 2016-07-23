using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Tic Tac Toe game");

            var gameController = new GameController();

            while (true)
            {
                Console.WriteLine("Whould you like to play new game? y/n");
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
