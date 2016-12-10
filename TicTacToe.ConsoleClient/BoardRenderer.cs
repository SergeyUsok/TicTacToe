using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Core;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.ConsoleClient
{
    static class BoardRenderer
    {
        private static readonly Dictionary<Mark, string> MarksMap = new Dictionary<Mark, string>()
                                                        {
                                                            {Mark.Cross, " X "},
                                                            {Mark.Empty, "   "},
                                                            {Mark.Nought, " 0 "}
                                                        };

        private static readonly Dictionary<string, ConsoleColor> ColorsMap = new Dictionary<string, ConsoleColor>
                                                        {
                                                            {" X ", ConsoleColor.Green},
                                                            {"   ", ConsoleColor.White},
                                                            {" 0 ", ConsoleColor.Blue}
                                                        };

        public static void DrawBoard(this Board board)
        {
            var horizontalBorder = GetHorizontalBorder(board);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(GetColumnsIndicies(board));
            Console.ResetColor();
            Console.WriteLine(horizontalBorder);

            for (int i = 0; i < board.Width; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(i + 1);
                Console.ResetColor();
                Console.Write(" |");

                for (int j = 0; j < board.Height; j++)
                {
                    var content = MarksMap[board[i, j]];
                    var color = ColorsMap[content];

                    // Draw mark with specific color
                    Console.ForegroundColor = color;
                    Console.Write(content);
                    Console.ResetColor();
                    
                    Console.Write("|");
                }

                Console.WriteLine();
                Console.WriteLine(horizontalBorder);
            }
        }

        public static void DrawBoard(this Board board, IList<Position> winRow)
        {
            var horizontalBorder = GetHorizontalBorder(board);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(GetColumnsIndicies(board));
            Console.ResetColor();
            Console.WriteLine(horizontalBorder);

            for (int i = 0; i < board.Width; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(i + 1);
                Console.ResetColor();
                Console.Write(" |");

                for (int j = 0; j < board.Height; j++)
                {
                    var content = MarksMap[board[i, j]];
                    var color = winRow.Any(cell => cell.X == i && cell.Y == j) ? ConsoleColor.Red : ColorsMap[content];

                    // Draw mark with specific color
                    Console.ForegroundColor = color;
                    Console.Write(content);
                    Console.ResetColor();

                    Console.Write("|");
                }

                Console.WriteLine();
                Console.WriteLine(horizontalBorder);
            }
        }

        private static string GetHorizontalBorder(Board board)
        {
            const int cellLength = 3; // each cell takes 3 symbols
            var numberOfCells = board.Width; // take width of board
            var borders = numberOfCells + 1; // number of borders is always greater than numberOfCells

            var symbolsInBorder = cellLength * numberOfCells + borders;

            return string.Format("  {0}", new string('-', symbolsInBorder));
        }

        private static string GetColumnsIndicies(Board board)
        {
            const string indexFormat = "  {0}  ";
            var builder = new StringBuilder();

            return Enumerable.Range(1, board.Width)
                          .Aggregate(builder, (sb, i) => sb.AppendFormat(indexFormat, i))
                          .ToString();
        }
    }
}
