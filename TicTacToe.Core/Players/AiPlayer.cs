using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Players
{
    public abstract class AiPlayer
    {
        protected readonly IGame Game;
        
        protected AiPlayer(IGame game, Mark myMark)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            if(myMark == Mark.Empty)
                throw new ArgumentException("Mark should be Cross or Nought");

            Game = game;
            MyMark = myMark;
        }

        public Mark MyMark { get; private set; }

        public abstract Move MakeMove();

        protected IEnumerable<Position> GetEmptyCells()
        {
            for (int x = 0; x < Game.Settings.Width; x++)
            {
                for (int y = 0; y < Game.Settings.Height; y++)
                {
                    if (Game.Board[x, y] == Mark.Empty)
                        yield return new Position(x, y);
                }
            }
        }

        protected Mark InvertMark(Mark playersMark)
        {
            if (playersMark == Mark.Cross)
                return Mark.Nought;

            return Mark.Cross;
        }

        protected Move GetRandomMove()
        {
            var emptyCells = GetEmptyCells().ToList();

            var cellIndex = DateTime.Now.Second % emptyCells.Count;

            var cell = emptyCells[cellIndex];

            var movement = Move.Make(cell.X, cell.Y, MyMark);

            return movement;
        }
    }
}