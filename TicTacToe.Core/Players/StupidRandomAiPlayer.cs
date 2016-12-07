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

        public override Move MakeMove()
        {
            var move = GetRandomMove();

            Game.Board[move.X, move.Y] = move.Mark;

            return move;
        }
    }
}
