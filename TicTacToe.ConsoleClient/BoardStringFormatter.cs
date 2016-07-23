using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.ConsoleClient
{
    static class BoardStringFormatter
    {
        private static Dictionary<Mark, string> _dictionary = new Dictionary<Mark, string>()
                                                        {
                                                            {Mark.Cross, " X "},
                                                            {Mark.Empty, "   "},
                                                            {Mark.Zero, " 0 "}
                                                        };

        public static string ToStringView(this Mark[,] board)
        {
            var horizontalBorder = GetHorizontalBorder(board);

            var builder = new StringBuilder();

            builder.AppendLine(GetColumnsIndicies(board));
            builder.AppendLine(horizontalBorder);

            for (int i = 0; i < board.GetLength(0); i++)
            {
                builder.AppendFormat("{0} |", i + 1);

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    builder.Append(_dictionary[board[i, j]]);
                    builder.Append("|");
                }

                builder.AppendLine();
                builder.AppendLine(horizontalBorder);
            }

            return builder.ToString();
        }

        private static string GetHorizontalBorder(Mark[,] board)
        {
            const int cell = 3; // each cell takes 3 symbols
            var numberOfCells = board.GetLength(0); // take width of board
            var borders = numberOfCells + 1; // number of borders is always greater than numberOfCells

            var symbolsInBorder = cell * numberOfCells + borders;

            return string.Format("  {0}", new string('-', symbolsInBorder));
        }

        private static string GetColumnsIndicies(Mark[,] board)
        {
            var indexFormat = "  {0}  ";
            var builder = new StringBuilder();

            return Enumerable.Range(1, board.GetLength(0))
                          .Aggregate(builder, (sb, i) => sb.AppendFormat(indexFormat, i))
                          .ToString();
        }
    }
}
