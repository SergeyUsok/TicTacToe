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

        // Calculate degree of usefulness for player and degree of sabotage for opponent player
        // the bigger number of cells in row then bigger degree of usefulness
        // the bigger number of cells in row we destroy for opponent then bigger degree of sabotage
        private KeyValuePair<int, Move> AssessMove(Position cell)
        {
            var myMove = Move.Make(cell.X, cell.Y, MyMark);
            var opponentsMove = Move.Make(cell.X, cell.Y, InvertMark(MyMark));

            // calculate usefulness and sabotage by pairs for each direction (vertical. horizontal etc.) 
            // and get pair with maximum sum
            var maximum = Maximum(
                                    CalculateHorizontalLine(myMove, opponentsMove), 
                                    CalculateVerticalLine(myMove, opponentsMove), 
                                    CalculateLeftDigonalLine(myMove, opponentsMove), 
                                    CalculateRightDigonalLine(myMove, opponentsMove)
                                 );

            return new KeyValuePair<int, Move>(maximum, myMove);
        }

            
        private int CalculateVerticalLine(Move myMove, Move opponentsMove)
        {
            var usefulnessDegree = Game.Board.VerticalInRow(myMove).Count;
            var sabotageDegree = Game.Board.VerticalInRow(opponentsMove).Count;

            return usefulnessDegree + sabotageDegree;
        }

        private int CalculateHorizontalLine(Move myMove, Move opponentsMove)
        {
            var usefulnessDegree = Game.Board.HorizontalInRow(myMove).Count;
            var sabotageDegree = Game.Board.HorizontalInRow(opponentsMove).Count;

            return usefulnessDegree + sabotageDegree;
        }

        private int CalculateLeftDigonalLine(Move myMove, Move opponentsMove)
        {
            var usefulnessDegree = Game.Board.LeftDigonalInRow(myMove).Count;
            var sabotageDegree = Game.Board.LeftDigonalInRow(opponentsMove).Count;

            return usefulnessDegree + sabotageDegree;
        }

        private int CalculateRightDigonalLine(Move myMove, Move opponentsMove)
        {
            var usefulnessDegree = Game.Board.RightDiagonalInRow(myMove).Count;
            var sabotageDegree = Game.Board.RightDiagonalInRow(opponentsMove).Count;

            return usefulnessDegree + sabotageDegree;
        }

        private int Maximum(params int[] assessments)
        {
            return assessments.Max();
        }
    }
}
