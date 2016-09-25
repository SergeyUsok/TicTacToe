using System;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core
{
    public static class Extensions
    {
        public static bool WithinBounds(this Mark[,] board, int x, int y)
        {
            if (board == null) 
                throw new ArgumentNullException("board");

            return x >= 0 &&
                   x < board.GetLength(0) &&
                   y >= 0 &&
                   y < board.GetLength(1);
        }

        public static bool IsEmpty(this Mark[,] board)
        {
            if (board == null) 
                throw new ArgumentNullException("board");

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != Mark.Empty)
                        return false;
                }
            }

            return true;
        }
    }
}
