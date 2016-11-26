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

        public override Movement MakeMove()
        {
            var movement = Game.Board.IsEmpty() ? GetRandomInitialMove() : GetMinimaxMove();

            Game.Board[movement.X, movement.Y] = movement.Mark;
            return movement;
        }

        private Movement GetRandomInitialMove()
        {
            var x = DateTime.Now.Second % Game.Settings.Width;
            var y = DateTime.Now.Second % Game.Settings.Height;

            return Movement.Make(x, y, MyMark);
        }

        private Movement GetMinimaxMove()
        {
            var maximum = int.MinValue;
            Movement movement = null;

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

        private int MiniMax(KeyValuePair<Movement, Mark[,]> move, Mark playersMark, int depth)
        {
            GameState result;
            if (IsTerminal(move, depth, out result))
            {
                return AssessMove(result, move.Key.Mark, depth);
            }

            var scores = GetMoves(move.Value, playersMark)
                        .AsParallel()
                        .Select(childMove => MiniMax(childMove, InvertMark(playersMark), depth + 1))
                        .ToList();

            return playersMark == MyMark ? scores.Max() : scores.Min();
        }

        private bool IsTerminal(KeyValuePair<Movement, Mark[,]> move, int depth, out GameState moveResult)
        {
            var depthReached = depth == _allowedDepth;

            var board = move.Value;

            moveResult = Game.GetMoveResult(board, move.Key).GameState;

            return moveResult != GameState.KeepPlaying || depthReached;
        }

        // Algorithm assess each move by giving Max to win move, 
        // Min to lost move and 0 to Draw or non-finished game
        // Algorithm also takes into account current considered Depth
        // Therefore Copmuter tries to finish game as fast as possible
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

        protected virtual IEnumerable<KeyValuePair<Movement, Mark[,]>> GetMoves(Mark[,] initialBoard, Mark playersMark)
        {
            for (int x = 0; x < Game.Settings.Width; x++)
            {
                for (int y = 0; y < Game.Settings.Height; y++)
                {
                    if (!CanBeProcessed(x, y, initialBoard))
                        continue; // skip already filled cells

                    var newBoard = CopyBoard(initialBoard);
                    newBoard[x, y] = playersMark;
                    yield return new KeyValuePair<Movement, Mark[,]>(Movement.Make(x, y, playersMark), newBoard);
                }
            }
        }

        protected virtual bool CanBeProcessed(int x, int y, Mark[,] board)
        {
            return board[x, y] == Mark.Empty;
        }

        private Mark[,] CopyBoard(Mark[,] sourceBoard)
        {
            var newBoard = new Mark[Game.Settings.Width, Game.Settings.Height];

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
