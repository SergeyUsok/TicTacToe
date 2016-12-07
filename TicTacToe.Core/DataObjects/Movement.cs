using System;

namespace TicTacToe.Core.DataObjects
{
    public class Move
    {
        public Move(int x, int y, Mark mark)
        {
            X = x;
            Y = y;

            if(mark == Mark.Empty)
                throw new ArgumentOutOfRangeException("mark", "Mark should be Nought or Cross");

            Mark = mark;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public Mark Mark { get; private set; }

        public static Move Make(int x, int y, Mark mark)
        {
            return new Move(x, y, mark);
        }
    }
}