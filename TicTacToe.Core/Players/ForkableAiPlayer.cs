using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Players
{
    public class MoveAssessment
    {
        public double Profit { get; set; }
        public double Danger { get; set; }
        public int Sum { get; set; }
        public Move Move { get; internal set; }
    }

    public class ForkableAiPlayer : AiPlayer
    {
        public ForkableAiPlayer(IGame game, Mark myMark) 
            : base(game, myMark)
        {
        }

        // simulating Human behavior when one makes decision how to move
        public override Move MakeMove()
        {
            var move = Game.Board.IsEmpty ? GetRandomMove() : GetMostProfitableMove();

            Game.Board[move.X, move.Y] = move.Mark;
            return move;
        }

        private Move GetMostProfitableMove()
        {
            var assessments = GetMoveAssessments(GetEmptyCells());

            // Check if there is an opponent's move with number of filled cells greater than 60% of required Number In Row to Win
            var assessmentWithMaxDanger = assessments.Aggregate((max, next) => max.Danger >= next.Danger ? max : next);

            if(assessmentWithMaxDanger.Danger > 60)
            {
                var assessmentWithMaxProfit = assessments.Aggregate((max, next) => max.Profit >= next.Profit ? max : next);

                if (assessmentWithMaxProfit.Profit >= assessmentWithMaxDanger.Danger)
                    return assessmentWithMaxProfit.Move;
                else
                    return assessmentWithMaxDanger.Move;
            }

            return assessments.Aggregate((max, next) => max.Sum >= next.Sum ? max : next).Move;
        }

        private IList<MoveAssessment> GetMoveAssessments(IEnumerable<Position> emptyCells)
        {
            var assessments = new List<MoveAssessment>();

            foreach (var cell in emptyCells)
            {
                var myMove = Move.Make(cell.X, cell.Y, MyMark);
                var opponentsMove = Move.Make(cell.X, cell.Y, InvertMark(MyMark));

                double profit;
                double danger;
                var sumAround = AssessMove(myMove, out profit) + AssessMove(opponentsMove, out danger);

                var result = new MoveAssessment { Move = myMove, Profit = profit, Danger = danger, Sum = sumAround };

                assessments.Add(result);
            }

            return assessments;
        }

        // Assess move in 2 ways:
        // 1) Sum all combinations in all 4 directions to get move profit
        // 2) What percentage was already filled of number in-row to win
        private int AssessMove(Move move, out double percentage)
        {
            var countInEachDirection = CountInEachDirection(move);

            percentage = countInEachDirection.Select(inRow => 100 * inRow / ((double)Game.Settings.NumberInRowToWin)).Max();

            return countInEachDirection.Sum();
        }

        private int[] CountInEachDirection(Move move)
        {
            return new[]
            {
                CountHorizontalLine(move),
                CountVerticalLine(move),
                CountLeftDigonalLine(move),
                CountRightDigonalLine(move)
            };
        }

        private int CountVerticalLine(Move move)
        {
            var cellsInRow = Game.Board.VerticalInRow(move, currentMark => currentMark == move.Mark).Count;

            // If there is more than 1 cell in-row check if it is possible to collect victory combination in given direction at all
            var possibleCount = Game.Board.VerticalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty).Count;

            // Check if current direction may lead to victory. If yes then real number of cells in row
            return possibleCount >= Game.Settings.NumberInRowToWin ? cellsInRow : 0;
        }

        private int CountHorizontalLine(Move move)
        {
            var cellsInRow = Game.Board.HorizontalInRow(move, currentMark => currentMark == move.Mark).Count;

            // If there is more than 1 cell in-row check if it is possible to collect victory combination in given direction at all
            var possibleCount = Game.Board.HorizontalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty).Count;

            // Check if current direction may lead to victory. If yes then real number of cells in row
            return possibleCount >= Game.Settings.NumberInRowToWin ? cellsInRow : 0;
        }

        private int CountLeftDigonalLine(Move move)
        {
            var cellsInRow = Game.Board.LeftDigonalInRow(move, currentMark => currentMark == move.Mark).Count;

            // If there is more than 1 cell in-row check if it is possible to collect victory combination in given direction at all
            var possibleCount = Game.Board.LeftDigonalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty).Count;

            // Check if current direction may lead to victory. If yes then real number of cells in row
            return possibleCount >= Game.Settings.NumberInRowToWin ? cellsInRow : 0;
        }

        private int CountRightDigonalLine(Move move)
        {
            var cellsInRow = Game.Board.RightDiagonalInRow(move, currentMark => currentMark == move.Mark).Count;

            // If there is more than 1 cell in-row check if it is possible to collect victory combination in given direction at all
            var possibleCount = Game.Board.RightDiagonalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty).Count;

            // Check if current direction may lead to victory. If yes then real number of cells in row
            return possibleCount >= Game.Settings.NumberInRowToWin ? cellsInRow : 0;
        }
    }
}
