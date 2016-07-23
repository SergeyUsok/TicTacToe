using System;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Players
{
    public abstract class Player
    {
        protected readonly GameParameters Parameters;
        protected readonly Mark MyMark;

        protected Player(GameParameters parameters, Mark myMark)
        {
            if (parameters == null) 
                throw new ArgumentNullException("parameters");

            if(myMark == Mark.Empty)
                throw new ArgumentException("Mark should be Cross or Zero");

            Parameters = parameters;
            MyMark = myMark;
        }

        public abstract void MakeMove(Mark[,] board);

        public event EventHandler<MoveEventArgs> Move;

        protected virtual void OnMove(MoveEventArgs args)
        {
            var handler = Move;
            if (handler != null) 
                handler(this, args);
        }
    }
}