using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core
{
    internal static class GameStateChecker
    {
        public static MoveResult GetMoveResult(Mark[,] board, Movement movement, int numberInRow)
        {
            return CheckForVictory(board, movement, numberInRow)
                       ? MoveResult.Victory
                       : CheckForDraw(board) 
                        ? MoveResult.Draw 
                        : MoveResult.KeepPlaying;
        }

        private static bool CheckForDraw(Mark[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == Mark.Empty)
                        return false;
                }
            }

            return true;
        }

        private static bool CheckForVictory(Mark[,] board, Movement movement, int numberInRow)
        {
            return CheckHorizontal(board, movement, numberInRow) ||
                   CheckVertical(board, movement, numberInRow) ||
                   CheckRightDiagonal(board, movement, numberInRow) ||
                   CheckLeftDigonal(board, movement, numberInRow);
        }

        private static bool CheckHorizontal(Mark[,] board, Movement movement, int numberInRow)
        {
            var countInRow = 1; // initial value equals 1 because at least 1 mark already was done by this Movement

            // traverse right from current point
            var nextX = movement.X + 1;

            while (WithinBounds(board, nextX, movement.Y) && board[nextX, movement.Y] == movement.Mark)
            {
                ++countInRow;
                nextX = nextX + 1;
            }

            // traverse left from current point
            nextX = movement.X - 1;

            while (WithinBounds(board, nextX, movement.Y) && board[nextX, movement.Y] == movement.Mark)
            {
                ++countInRow;
                nextX = nextX - 1;
            }

            return countInRow == numberInRow;
        }

        private static bool CheckVertical(Mark[,] board, Movement movement, int numberInRow)
        {
            var countInRow = 1; // initial value equals 1 because at least 1 mark already was done by this Movement

            // traverse up from current point
            var nextY = movement.Y - 1;

            while (WithinBounds(board, movement.X, nextY) && board[movement.X, nextY] == movement.Mark)
            {
                ++countInRow;
                nextY = nextY - 1;
            }

            // traverse down from current point
            nextY = movement.Y + 1;

            while (WithinBounds(board, movement.X, nextY) && board[movement.X, nextY] == movement.Mark)
            {
                ++countInRow;
                nextY = nextY + 1;
            }

            return countInRow == numberInRow;
        }

        // Check whether right digonal is filled. Example:
        //   0 x
        // 0 x 
        // x 0 
        private static bool CheckRightDiagonal(Mark[,] board, Movement movement, int numberInRow)
        {
            var countInRow = 1; // initial value equals 1 because at least 1 mark already was done by this Movement

            // traverse down-left from current point
            var nextX = movement.X - 1;
            var nextY = movement.Y + 1;

            while (WithinBounds(board, nextX, nextY) && board[nextX, nextY] == movement.Mark)
            {
                ++countInRow;
                nextX = nextX - 1;
                nextY = nextY + 1;
            }

            // traverse up-right from current point
            nextX = movement.X + 1;
            nextY = movement.Y - 1;

            while (WithinBounds(board, nextX, nextY) && board[nextX, nextY] == movement.Mark)
            {
                ++countInRow;
                nextX = nextX + 1;
                nextY = nextY - 1;
            }

            return countInRow == numberInRow;
        }

        // Check whether left digonal is filled. Example:
        // x 0
        // 0 x 
        //   0 x
        private static bool CheckLeftDigonal(Mark[,] board, Movement movement, int numberInRow)
        {
            var countInRow = 1; // initial value equals 1 because at least 1 mark already was done by this Movement

            // traverse up-left from current point
            var nextX = movement.X - 1;
            var nextY = movement.Y - 1;

            while (WithinBounds(board, nextX, nextY) && board[nextX, nextY] == movement.Mark)
            {
                ++countInRow;
                nextX = nextX - 1;
                nextY = nextY - 1;
            }

            // traverse down-right from current point
            nextX = movement.X + 1;
            nextY = movement.Y + 1;

            while (WithinBounds(board, nextX, nextY) && board[nextX, nextY] == movement.Mark)
            {
                ++countInRow;
                nextX = nextX + 1;
                nextY = nextY + 1;
            }

            return countInRow == numberInRow;
        }

        private static bool WithinBounds(Mark[,] board, int x, int y)
        {
            return x >= 0 && 
                   x < board.GetLength(0) && 
                   y >= 0 && 
                   y < board.GetLength(1);
        }
    }
}
