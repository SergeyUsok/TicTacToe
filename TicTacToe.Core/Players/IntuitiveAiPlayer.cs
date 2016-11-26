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
        public override Movement MakeMove()
        {
            // 1. get all empty cells where move is possible
            var emptyCells = GetEmptyCells().ToList();

            var movement = GetWinMove(emptyCells) ??    // 2. Looking if Victory is possible by any move. If any move in that way
                           GetPreventionMove(emptyCells) ?? // 3. If no Victory moves available check for opponent's Victory and try prevent it
                           GetRandomMove(emptyCells); // 4. If nothing to prevent move randomly 

            Game.Board[movement.X, movement.Y] = movement.Mark;
            return movement;
        }

        private int AssessMove()
        {
            
        }










        private Movement GetRandomMove(List<Cell> emptyCells)
        {
            var cellIndex = DateTime.Now.Second % emptyCells.Count;

            var cell = emptyCells[cellIndex];

            return Movement.Make(cell.X, cell.Y, MyMark);
        }

        private Movement GetPreventionMove(IEnumerable<Cell> emptyCells)
        {
            var victoryMoves = GetVictoryMoves(emptyCells, InvertMark(MyMark));

            return victoryMoves.Any()
                       ? Movement.Make(victoryMoves.First().X, victoryMoves.First().Y, MyMark)
                       : null;
        }

        private Movement GetWinMove(IEnumerable<Cell> emptyCells)
        {
            var victoryMoves = GetVictoryMoves(emptyCells, MyMark);

            return victoryMoves.Any() 
                        ? Movement.Make(victoryMoves.First().X, victoryMoves.First().Y, MyMark) 
                        : null;
        }

        private IList<Cell> GetVictoryMoves(IEnumerable<Cell> emptyCells, Mark mark)
        {
            return emptyCells.Select(emptyCell => new 
                                                    {
                                                        emptyCell,
                                                        moveResult = Game.GetMoveResult(Movement.Make(emptyCell.X, emptyCell.Y, mark))
                                                    })
                              .Where(@t => @t.moveResult.GameState == GameState.Victory)
                              .Select(@t => @t.emptyCell)
                              .ToList();
        }
    }
}
