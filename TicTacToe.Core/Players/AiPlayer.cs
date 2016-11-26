using System;
using System.Collections.Generic;
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

        public abstract Movement MakeMove();

        protected IEnumerable<Cell> GetEmptyCells()
        {
            for (int x = 0; x < Game.Settings.Width; x++)
            {
                for (int y = 0; y < Game.Settings.Height; y++)
                {
                    if (Game.Board[x, y] == Mark.Empty)
                        yield return new Cell(x, y);
                }
            }
        }

        protected Mark InvertMark(Mark playersMark)
        {
            if (playersMark == Mark.Cross)
                return Mark.Nought;

            return Mark.Cross;
        }
    }
}