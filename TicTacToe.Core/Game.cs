using System;
using System.Collections.Generic;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core
{
    public sealed class Game : IGame
    {
        public Game(GameSettings settings)
        {
            if (settings == null) 
                throw new ArgumentNullException("settings");

            Settings = settings;
            Board = new Mark[settings.Width, settings.Height];
        }

        // Made internal for test purposes
        public Mark[,] Board { get; internal set; }

        public GameSettings Settings { get; private set; }

        public MoveResult GetMoveResult(Movement movement)
        {
            return GetMoveResult(Board, movement);
        }

        public MoveResult GetMoveResult(Mark[,] board, Movement movement)
        {
            var moveResult = new MoveResult {Mark = movement.Mark};

            List<Cell> row = null;
            
            var state = CheckForVictory(board, movement, out row)
                       ? GameState.Victory
                       : CheckForDraw(board)
                        ? GameState.Draw
                        : GameState.KeepPlaying;

            moveResult.GameState = state;
            moveResult.WinRow = row;

            return moveResult;
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

        private bool CheckForVictory(Mark[,] board, Movement movement, out List<Cell> row)
        {
            row = CheckHorizontal(board, movement) ??
                   CheckVertical(board, movement) ??
                   CheckRightDiagonal(board, movement) ??
                   CheckLeftDigonal(board, movement);

            return row != null;
        }

        private List<Cell> CheckHorizontal(Mark[,] board, Movement movement)
        {
            var winRow = board.CountHorizontal(movement);
            
            return winRow.Count == Settings.NumberInRowToWin ? winRow : null;
        }

        private List<Cell> CheckVertical(Mark[,] board, Movement movement)
        {
            var winRow = board.CountVertical(movement);

            return winRow.Count == Settings.NumberInRowToWin ? winRow : null;
        }

        // Check whether right digonal is filled. Example:
        //   0 x
        // 0 x 
        // x 0 
        private List<Cell> CheckRightDiagonal(Mark[,] board, Movement movement)
        {
            var winRow = board.CountRightDiagonal(movement);

            return winRow.Count == Settings.NumberInRowToWin ? winRow : null;
        }

        // Check whether left digonal is filled. Example:
        // x 0
        // 0 x 
        //   0 x
        private List<Cell> CheckLeftDigonal(Mark[,] board, Movement movement)
        {
            var winRow = board.CountLeftDigonal(movement);

            return winRow.Count == Settings.NumberInRowToWin ? winRow : null;
        }
    }
}
