using System;

namespace TicTacToe.Core.DataObjects
{
    public class GameParameters
    {
        public GameParameters(int width, int height, int numberInRowToWin)
        {
            if (numberInRowToWin > height || numberInRowToWin > width)
                throw new ArgumentOutOfRangeException("numberInRowToWin", "Provided numberInRowToWin is greater than board size");

            Width = width;
            Height = height;
            NumberInRowToWin = numberInRowToWin;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int NumberInRowToWin { get; private set; }
    }
}
