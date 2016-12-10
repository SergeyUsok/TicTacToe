using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Players
{
    public class MiniMaxAiPlayer : AiPlayer
    {
        // Constants for assessments of possible moves
        private const int WinAssessment = 1000;
        private const int LostAssessment = -1000;
        private const int NeutralAssessment = 0;

        private readonly int _allowedDepth;
        
        public MiniMaxAiPlayer(IGame game, Mark myMark, int depth) 
            : base(game, myMark)
        {
            if (depth < 0) 
                throw new ArgumentOutOfRangeException("depth");

            _allowedDepth = depth;
        }

        public override Move MakeMove()
        {
            var move = Game.Board.IsEmpty ? GetRandomMove() : GetMinimaxMove();

            Game.Board[move.X, move.Y] = move.Mark;
            return move;
        }

        private Move GetMinimaxMove()
        {
            var maximum = int.MinValue;
            Move movement = null;

            foreach (var move in GetMoves(Game.Board, MyMark))
            {
                var score = MiniMax(move, InvertMark(MyMark), 1);

                if (score <= maximum)
                    continue;

                maximum = score;
                movement = move.Key;
            }

            return movement;
        }

        private int MiniMax(KeyValuePair<Move, Board> move, Mark playersMark, int depth)
        {
            GameState result;
            if (IsTerminal(move, depth, out result))
            {
                return AssessMove(result, move.Key.Mark, depth);
            }

            var scores = GetMoves(move.Value, playersMark)
                        .Select(childMove => MiniMax(childMove, InvertMark(playersMark), depth + 1))
                        .ToList();

            return playersMark == MyMark ? scores.Max() : scores.Min();
        }

        private bool IsTerminal(KeyValuePair<Move, Board> move, int depth, out GameState moveResult)
        {
            var depthReached = depth == _allowedDepth;

            var board = move.Value;

            moveResult = Game.GetMoveResult(board, move.Key).GameState;

            return moveResult != GameState.KeepPlaying || depthReached;
        }

        // Algorithm assess each move by giving Max to win move, 
        // Min to lost move and 0 to Draw or non-finished game
        // Algorithm also takes into account current considered Depth
        // Therefore Computer tries to finish game as fast as possible
        // and loose as later as possible
        // example: http://neverstopbuilding.com/minimax
        protected virtual int AssessMove(GameState moveResult, Mark playersMark, int depth)
        {
            var winAtGivingDepth = WinAssessment - depth;
            var lostAtGivingDepth = LostAssessment + depth;
            var neutralAtGivingDepth = NeutralAssessment - depth;

            return moveResult == GameState.Victory
                        // if move leads to Victory check who is winner and assess the move accordingly
                       ? (playersMark == MyMark ? winAtGivingDepth : lostAtGivingDepth)
                       // in other case assess move as neutral
                       : neutralAtGivingDepth;
        }

        protected virtual IEnumerable<KeyValuePair<Move, Board>> GetMoves(Board initialBoard, Mark playersMark)
        {
            for (int x = 0; x < Game.Settings.Width; x++)
            {
                for (int y = 0; y < Game.Settings.Height; y++)
                {
                    if (!CanBeProcessed(x, y, initialBoard))
                        continue; // skip already filled cells

                    var newBoard = CopyBoard(initialBoard);
                    newBoard[x, y] = playersMark;
                    yield return new KeyValuePair<Move, Board>(Move.Make(x, y, playersMark), newBoard);
                }
            }
        }

        protected virtual bool CanBeProcessed(int x, int y, Board board)
        {
            return board[x, y] == Mark.Empty;
        }

        private Board CopyBoard(Board sourceBoard)
        {
            var newBoard = new Board(Game.Settings.Width, Game.Settings.Height);

            for (int x = 0; x < sourceBoard.Width; x++)
            {
                for (int y = 0; y < sourceBoard.Height; y++)
                {
                    newBoard[x, y] = sourceBoard[x, y];
                }
            }

            return newBoard;
        }
    }
}
