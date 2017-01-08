using System;
using System.Collections.Generic;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core
{
    public static class BoardExtensions
    {
        public static bool WithinBounds(this Board board, int x, int y)
        {
            if (board == null) 
                throw new ArgumentNullException("board");

            return x >= 0 &&
                   x < board.Width &&
                   y >= 0 &&
                   y < board.Height;
        }

        internal static List<Position> HorizontalInRow(this Board board, Move movement, Func<Mark, bool> predicate)
        {
            var inRow = new List<Position> { new Position(movement.X, movement.Y) };

            // traverse to right from current point
            var nextX = movement.X + 1;
            //board[nextX, movement.Y] == movement.Mark
            while (board.WithinBounds(nextX, movement.Y) && predicate(board[nextX, movement.Y]))
            {
                inRow.Add(new Position(nextX, movement.Y));
                nextX = nextX + 1;
            }

            // traverse to left from current point
            nextX = movement.X - 1;
            //board[nextX, movement.Y] == movement.Mark
            while (board.WithinBounds(nextX, movement.Y) && predicate(board[nextX, movement.Y]))
            {
                inRow.Add(new Position(nextX, movement.Y));
                nextX = nextX - 1;
            }

            return inRow;
        }

        internal static List<Position> VerticalInRow(this Board board, Move movement, Func<Mark, bool> predicate)
        {
            var inRow = new List<Position> { new Position(movement.X, movement.Y) };

            // traverse up from current point
            var nextY = movement.Y - 1;
            //board[movement.X, nextY] == movement.Mark
            while (board.WithinBounds(movement.X, nextY) && predicate(board[movement.X, nextY]))
            {
                inRow.Add(new Position(movement.X, nextY));
                nextY = nextY - 1;
            }

            // traverse down from current point
            nextY = movement.Y + 1;
            //board[movement.X, nextY] == movement.Mark
            while (board.WithinBounds(movement.X, nextY) && predicate(board[movement.X, nextY]))
            {
                inRow.Add(new Position(movement.X, nextY));
                nextY = nextY + 1;
            }

            return inRow;
        }

        internal static List<Position> RightDiagonalInRow(this Board board, Move movement, Func<Mark, bool> predicate)
        {
            var inRow = new List<Position> { new Position(movement.X, movement.Y) };

            // traverse down-left from current point
            var nextX = movement.X - 1;
            var nextY = movement.Y + 1;
            //board[nextX, nextY] == movement.Mark
            while (board.WithinBounds(nextX, nextY) && predicate(board[nextX, nextY]))
            {
                inRow.Add(new Position(nextX, nextY));
                nextX = nextX - 1;
                nextY = nextY + 1;
            }

            // traverse up-right from current point
            nextX = movement.X + 1;
            nextY = movement.Y - 1;
            //board[nextX, nextY] == movement.Mark
            while (board.WithinBounds(nextX, nextY) && predicate(board[nextX, nextY]))
            {
                inRow.Add(new Position(nextX, nextY));
                nextX = nextX + 1;
                nextY = nextY - 1;
            }

            return inRow;
        }

        internal static List<Position> LeftDigonalInRow(this Board board, Move movement, Func<Mark, bool> predicate)
        {
            var inRow = new List<Position> { new Position(movement.X, movement.Y) };

            // traverse up-left from current point
            var nextX = movement.X - 1;
            var nextY = movement.Y - 1;
            //board[nextX, nextY] == movement.Mark
            while (board.WithinBounds(nextX, nextY) && predicate(board[nextX, nextY]))
            {
                inRow.Add(new Position(nextX, nextY));
                nextX = nextX - 1;
                nextY = nextY - 1;
            }

            // traverse down-right from current point
            nextX = movement.X + 1;
            nextY = movement.Y + 1;
            //board[nextX, nextY] == movement.Mark
            while (board.WithinBounds(nextX, nextY) && predicate(board[nextX, nextY]))
            {
                inRow.Add(new Position(nextX, nextY));
                nextX = nextX + 1;
                nextY = nextY + 1;
            }

            return inRow;
        }
    }
}
