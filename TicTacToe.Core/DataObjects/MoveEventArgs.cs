using System;

namespace TicTacToe.Core.DataObjects
{
    public class MoveEventArgs : EventArgs
    {
        public Movement Movement { get; private set; }

        public MoveEventArgs(Movement movement)
        {
            if (movement == null) 
                throw new ArgumentNullException("movement");

            Movement = movement;
        }
    }
}
