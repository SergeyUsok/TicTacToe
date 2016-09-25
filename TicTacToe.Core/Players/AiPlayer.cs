using System;
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
    }
}