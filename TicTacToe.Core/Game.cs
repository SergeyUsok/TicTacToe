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
            Board = new Board(settings.Width, settings.Height);
        }

        // Made internal for test purposes
        public Board Board { get; internal set; }

        public GameSettings Settings { get; private set; }

        public MoveResult GetMoveResult(Move movement)
        {
            return GetMoveResult(Board, movement);
        }

        public MoveResult GetMoveResult(Board board, Move movement)
        {
            var moveResult = new MoveResult {Mark = movement.Mark};

            List<Position> row = null;
            
            var state = CheckForVictory(board, movement, out row)
                       ? GameState.Victory
                       : CheckForDraw(board)
                        ? GameState.Draw
                        : GameState.KeepPlaying;

            moveResult.GameState = state;
            moveResult.WinRow = row;

            return moveResult;
        }

        private static bool CheckForDraw(Board board)
        {
            for(int x = 0; x < board.Width; x++)
            {
                for(int y = 0; y < board.Height; y++)
                {
                    if (board[x, y] == Mark.Empty)
                        return false;
                }
            }

            return true;
        }

        private bool CheckForVictory(Board board, Move movement, out List<Position> row)
        {
            row = CheckHorizontal(board, movement) ??
                   CheckVertical(board, movement) ??
                   CheckRightDiagonal(board, movement) ??
                   CheckLeftDigonal(board, movement);

            return row != null;
        }

        private List<Position> CheckHorizontal(Board board, Move movement)
        {
            var winRow = board.HorizontalInRow(movement);
            
            return winRow.Count == Settings.NumberInRowToWin ? winRow : null;
        }

        private List<Position> CheckVertical(Board board, Move movement)
        {
            var winRow = board.VerticalInRow(movement);

            return winRow.Count == Settings.NumberInRowToWin ? winRow : null;
        }

        // Check whether right diagonal is filled. Example:
        //   0 x
        // 0 x 
        // x 0 
        private List<Position> CheckRightDiagonal(Board board, Move movement)
        {
            var winRow = board.RightDiagonalInRow(movement);

            return winRow.Count == Settings.NumberInRowToWin ? winRow : null;
        }

        // Check whether left diagonal is filled. Example:
        // x 0
        // 0 x 
        //   0 x
        private List<Position> CheckLeftDigonal(Board board, Move movement)
        {
            var winRow = board.LeftDigonalInRow(movement);

            return winRow.Count == Settings.NumberInRowToWin ? winRow : null;
        }
    }
}
