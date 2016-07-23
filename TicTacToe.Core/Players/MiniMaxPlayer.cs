using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Players
{
    public class MiniMaxPlayer : Player
    {
        private readonly int _allowedDepth;
        
        public MiniMaxPlayer(GameParameters parameters, Mark myMark, int depth) 
            : base(parameters, myMark)
        {
            if (depth < 0) 
                throw new ArgumentOutOfRangeException("depth");

            _allowedDepth = depth;
        }

        public override void MakeMove(Mark[,] board)
        {
            var maximum = int.MinValue;
            Movement movement = null;

            foreach (var move in GetMoves(board, MyMark))
            {
                var score = MiniMax(move, InvertMark(MyMark), 1);

                if (score <= maximum)
                    continue;

                maximum = score;
                movement = move.Key;
            }

            OnMove(new MoveEventArgs(movement));
        }

        private int MiniMax(KeyValuePair<Movement, Mark[,]> move, Mark playersMark, int depth)
        {
            if (IsTerminal(move.Value, depth))
            {
                var a = AssessMove(move);
                return a;
            }
                
            var scores = GetMoves(move.Value, playersMark)
                .Select(childMove => MiniMax(childMove, InvertMark(playersMark), depth + 1))
                .ToList();

            return playersMark == MyMark ? scores.Max() : scores.Min();
        }

        private Mark InvertMark(Mark playersMark)
        {
            if (playersMark == Mark.Cross)
                return Mark.Zero;

            return Mark.Cross;
        }

        private bool IsTerminal(Mark[,] board, int depth)
        {
            var isDepthReached = depth == _allowedDepth;

            if (isDepthReached)
                return true;

            for (int row = 0; row < Parameters.Height; row++)
            {
                for (int column = 0; column < Parameters.Width; column++)
                {
                    if (board[row, column] == Mark.Empty)
                        return false;
                }
            }

            return true;
        }

        private int AssessMove(KeyValuePair<Movement, Mark[,]> move)
        {
            const int winAssessment = 10;
            const int lostAssessment = -10;
            const int drawAssessment = 0;
            const int neutralAssessment = 0;

            var moveResult = GameStateChecker.GetMoveResult(move.Value, move.Key, Parameters.NumberInRowToWin);

            switch (moveResult)
            {
                case MoveResult.KeepPlaying:
                    return neutralAssessment;
                case MoveResult.Draw:
                    return drawAssessment;
                case MoveResult.Victory:
                    return move.Key.Mark == MyMark ? winAssessment : lostAssessment;
            }

            return neutralAssessment;
        }

        private IEnumerable<KeyValuePair<Movement, Mark[,]>> GetMoves(Mark[,] initialBoard, Mark playersMark)
        {
            for (int x = 0; x < Parameters.Width; x++)
            {
                for (int y = 0; y < Parameters.Height; y++)
                {
                    if (initialBoard[x, y] != Mark.Empty)
                        continue; // skip already filled cells

                    var newBoard = CopyBoard(initialBoard);
                    newBoard[x, y] = playersMark;
                    yield return new KeyValuePair<Movement, Mark[,]>(Movement.Make(x, y, playersMark), newBoard);
                }
            }
        }

        private Mark[,] CopyBoard(Mark[,] sourceBoard)
        {
            var newBoard = new Mark[Parameters.Height, Parameters.Width];

            for (int x = 0; x < sourceBoard.GetLength(0); x++)
            {
                for (int y = 0; y < sourceBoard.GetLength(1); y++)
                {
                    newBoard[x, y] = sourceBoard[x, y];
                }
            }

            return newBoard;
        }
    }
}
