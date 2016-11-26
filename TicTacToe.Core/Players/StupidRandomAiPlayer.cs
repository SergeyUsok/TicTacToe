using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Players
{
    public class StupidRandomAiPlayer : AiPlayer
    {
        public StupidRandomAiPlayer(IGame game, Mark myMark) 
            : base(game, myMark)
        {
        }

        public override Movement MakeMove()
        {
            var emptyCells = GetEmptyCells().ToList();

            var cellIndex = DateTime.Now.Second % emptyCells.Count;

            var cell = emptyCells[cellIndex];

            var movement = Movement.Make(cell.X, cell.Y, MyMark);

            Game.Board[movement.X, movement.Y] = movement.Mark;

            return movement;
        }
    }
}
