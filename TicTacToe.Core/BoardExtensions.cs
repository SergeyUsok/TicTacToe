using System;
using System.Collections.Generic;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core
{
    public static class BoardExtensions
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

        internal static bool IsEmpty(this Mark[,] board)
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

        internal static List<Cell> CountHorizontal(this Mark[,] board, Movement movement)
        {
            var inRow = new List<Cell> { new Cell(movement.X, movement.Y) };

            // traverse to right from current point
            var nextX = movement.X + 1;

            while (board.WithinBounds(nextX, movement.Y) && board[nextX, movement.Y] == movement.Mark)
            {
                inRow.Add(new Cell(nextX, movement.Y));
                nextX = nextX + 1;
            }

            // traverse to left from current point
            nextX = movement.X - 1;

            while (board.WithinBounds(nextX, movement.Y) && board[nextX, movement.Y] == movement.Mark)
            {
                inRow.Add(new Cell(nextX, movement.Y));
                nextX = nextX - 1;
            }

            return inRow;
        }

        internal static List<Cell> CountVertical(this Mark[,] board, Movement movement)
        {
            var inRow = new List<Cell> { new Cell(movement.X, movement.Y) };

            // traverse up from current point
            var nextY = movement.Y - 1;

            while (board.WithinBounds(movement.X, nextY) && board[movement.X, nextY] == movement.Mark)
            {
                inRow.Add(new Cell(movement.X, nextY));
                nextY = nextY - 1;
            }

            // traverse down from current point
            nextY = movement.Y + 1;

            while (board.WithinBounds(movement.X, nextY) && board[movement.X, nextY] == movement.Mark)
            {
                inRow.Add(new Cell(movement.X, nextY));
                nextY = nextY + 1;
            }

            return inRow;
        }

        internal static List<Cell> CountRightDiagonal(this Mark[,] board, Movement movement)
        {
            var inRow = new List<Cell> { new Cell(movement.X, movement.Y) };

            // traverse down-left from current point
            var nextX = movement.X - 1;
            var nextY = movement.Y + 1;

            while (board.WithinBounds(nextX, nextY) && board[nextX, nextY] == movement.Mark)
            {
                inRow.Add(new Cell(nextX, nextY));
                nextX = nextX - 1;
                nextY = nextY + 1;
            }

            // traverse up-right from current point
            nextX = movement.X + 1;
            nextY = movement.Y - 1;

            while (board.WithinBounds(nextX, nextY) && board[nextX, nextY] == movement.Mark)
            {
                inRow.Add(new Cell(nextX, nextY));
                nextX = nextX + 1;
                nextY = nextY - 1;
            }

            return inRow;
        }

        internal static List<Cell> CountLeftDigonal(this Mark[,] board, Movement movement)
        {
            var inRow = new List<Cell> { new Cell(movement.X, movement.Y) };

            // traverse up-left from current point
            var nextX = movement.X - 1;
            var nextY = movement.Y - 1;

            while (board.WithinBounds(nextX, nextY) && board[nextX, nextY] == movement.Mark)
            {
                inRow.Add(new Cell(nextX, nextY));
                nextX = nextX - 1;
                nextY = nextY - 1;
            }

            // traverse down-right from current point
            nextX = movement.X + 1;
            nextY = movement.Y + 1;

            while (board.WithinBounds(nextX, nextY) && board[nextX, nextY] == movement.Mark)
            {
                inRow.Add(new Cell(nextX, nextY));
                nextX = nextX + 1;
                nextY = nextY + 1;
            }

            return inRow;
        }
    }
}
