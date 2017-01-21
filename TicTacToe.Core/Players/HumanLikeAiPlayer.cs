using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Players
{
    public class HumanLikeAiPlayer : AiPlayer
    {
        public HumanLikeAiPlayer(IGame game, Mark myMark)
            : base(game, myMark)
        {
        }

        // simulating Human behavior when one makes decision how to move
        public override Move MakeMove()
        {
            var move = Game.Board.IsEmpty ? GetRandomMove() : ChooseMove();

            Game.Board[move.X, move.Y] = move.Mark;
            return move;
        }

        private Move ChooseMove()
        {
            double profitLevel;
            var myMove = GetMostProfitableMove(GetEmptyCells(), MyMark, out profitLevel);

            double dangerLevel;
            var opponentsMove = GetMostProfitableMove(GetEmptyCells(), InvertMark(MyMark), out dangerLevel);

            // If number of opponent's filled cells greater than 60% from required Number In Row to Win
            // And this number greater then number of self filled cells
            if (dangerLevel > 60 && dangerLevel > profitLevel)
                return Move.Make(opponentsMove.X, opponentsMove.Y, MyMark);
            else
                return myMove;
        }

        // TODO if several maximum moves are present then add some choose logic
        private Move GetMostProfitableMove(IEnumerable<Position> emptyCells, Mark mark,  out double assessment)
        {
            assessment = 0;
            Move move = null;

            foreach (var cell in emptyCells)
            {
                var currentMove = Move.Make(cell.X, cell.Y, mark);

                var currentAssessment = AssessMove(currentMove);

                if(assessment <= currentAssessment)
                {
                    assessment = currentAssessment;
                    move = currentMove;
                }
            }

            return move;
        }

        // Assess move by getting maximum filled cells in row
        // and calculating percent of filled cells for victory 
        private double AssessMove(Move move)
        {
            var maxCombination = Maximum(
                                         CountHorizontalLine(move),
                                         CountVerticalLine(move),
                                         CountLeftDigonalLine(move),
                                         CountRightDigonalLine(move)
                                        );

            return 100 * maxCombination / ((double)Game.Settings.NumberInRowToWin);
        }
        
        private int CountVerticalLine(Move move)
        {
            var cellsInRow = Game.Board.VerticalInRow(move, currentMark => currentMark == move.Mark).Count;

            // since current move also calculated as a part of cells in-row
            // do not take it into account because single cell is not playing any role
            if (cellsInRow == 1)
                return 0;

            // If there is more than 1 cell in-row check if it is possible to collect victory combination in given direction at all
            var possibleCount = Game.Board.VerticalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty).Count;

            // If yes then return real number of cells in row
            return possibleCount >= Game.Settings.NumberInRowToWin ? cellsInRow : 0;
        }

        private int CountHorizontalLine(Move move)
        {
            var cellsInRow = Game.Board.HorizontalInRow(move, currentMark => currentMark == move.Mark).Count;

            // since current move also calculated as a part of cells in-row
            // do not take it into account because single cell is not playing any role
            if (cellsInRow == 1)
                return 0;

            // If there is more than 1 cell in-row check if it is possible to collect victory combination in given direction at all
            var possibleCount = Game.Board.HorizontalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty).Count;

            // If yes then return real number of cells in row
            return possibleCount >= Game.Settings.NumberInRowToWin ? cellsInRow : 0;
        }

        private int CountLeftDigonalLine(Move move)
        {
            var cellsInRow = Game.Board.LeftDigonalInRow(move, currentMark => currentMark == move.Mark).Count;

            // since current move also calculated as a part of cells in-row
            // do not take it into account because single cell is not playing any role
            if (cellsInRow == 1)
                return 0;

            // If there is more than 1 cell in-row check if it is possible to collect victory combination in given direction at all
            var possibleCount = Game.Board.LeftDigonalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty).Count;

            // If yes then return real number of cells in row
            return possibleCount >= Game.Settings.NumberInRowToWin ? cellsInRow : 0;
        }

        private int CountRightDigonalLine(Move move)
        {
            var cellsInRow = Game.Board.RightDiagonalInRow(move, currentMark => currentMark == move.Mark).Count;

            // since current move also calculated as a part of cells in-row
            // do not take it into account because single cell is not playing any role
            if (cellsInRow == 1)
                return 0;

            // If there is more than 1 cell in-row check if it is possible to collect victory combination in given direction at all
            var possibleCount = Game.Board.RightDiagonalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty).Count;

            // If yes then return real number of cells in row
            return possibleCount >= Game.Settings.NumberInRowToWin ? cellsInRow : 0;
        }

        private int Maximum(params int[] inRowInEachDirection)
        {
            return inRowInEachDirection.Max();
        }
    }
}
