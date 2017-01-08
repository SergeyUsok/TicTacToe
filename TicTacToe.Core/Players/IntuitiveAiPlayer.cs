using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Players
{
    public class IntuitiveAiPlayer : AiPlayer
    {
        public IntuitiveAiPlayer(IGame game, Mark myMark) 
            : base(game, myMark)
        {
        }

        // simulating Human behavior when one makes decision how to move
        public override Move MakeMove()
        {
            var move = Game.Board.IsEmpty ? GetRandomMove()
                                          : GetEmptyCells().Select(cell => AssessMove(cell))
                                                           .Aggregate((max, next) => max.Key >= next.Key ? max : next) // since there is no out-of-box method MaxBy use Aggregate then
                                                           .Value;

            Game.Board[move.X, move.Y] = move.Mark;
            return move;
        }

        // By calculating the profit of move we solve following:
        // 1. How big usefulness of this move for me?
        // 2. How big will be damage to opponent if I put Mark there and sabotage opponent's move?
        private KeyValuePair<double, Move> AssessMove(Position cell)
        {
            var myMove = Move.Make(cell.X, cell.Y, MyMark);
            var opponentsMove = Move.Make(cell.X, cell.Y, InvertMark(MyMark));

            // my move coefficient is greater than opponents one in order to prioritize them
            // collect greater moves in row has more priority than sabotage opponents collection
            const double myMoveCoefficient = 1.1;
            const double opponentsMoveCoefficient = 1.0;

            var myProfit = CalculateMoveProfit(myMove, myMoveCoefficient);
            var opponentsProfit = CalculateMoveProfit(opponentsMove, opponentsMoveCoefficient);

            var totalSum = myProfit + opponentsProfit;

            return new KeyValuePair<double, Move>(totalSum, myMove);
        }

        // Calculate profit of move by getting maximum number of Marks in row
        // and applying this result to NumberInRowToWin setting
        private double CalculateMoveProfit(Move move, double playerCoefficient)
        {
            var moveAssessment = Summarize(
                                    HorizontalLine(move),
                                    VerticalLine(move),
                                    LeftDigonalLine(move),
                                    RightDigonalLine(move)
                                 );

            return moveAssessment * playerCoefficient;
        }

        private List<Position> VerticalLine(Move move)
        {
            return Game.Board.VerticalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty);
        }

        private List<Position> HorizontalLine(Move move)
        {
            return Game.Board.HorizontalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty);
        }

        private List<Position> LeftDigonalLine(Move move)
        {
            return Game.Board.LeftDigonalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty);
        }

        private List<Position> RightDigonalLine(Move move)
        {
            return Game.Board.RightDiagonalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty);
        }

        private double Calculate(List<Position> positions)
        {
            // no sense to calculate if given direction cannot lead to victory due to lack of available cells
            if (positions.Count < Game.Settings.NumberInRowToWin)
                return 0;

            var realMoves = positions.Count(p => Game.Board[p.X, p.Y] != Mark.Empty);
            var potentialMoves = positions.Count(p => Game.Board[p.X, p.Y] == Mark.Empty);

            // obtain coefficient for multiplication on number of real (filled) moves
            // smaller number of potential moves (and greater number of real ones) greater coefficient and result
            var coefficient = Game.Settings.NumberInRowToWin / potentialMoves;

            // Sum of real moves, e.g. if count of real moves will be 3 then sum is: 1+2+3
            // multiply sum on calculated coefficient
            return Enumerable.Range(1, realMoves).Sum() * coefficient;
        }
        
        private double Summarize(params List<Position>[] inRowInEachDirection)
        {
            return inRowInEachDirection.Sum(positions => Calculate(positions));
        }
    }
}
