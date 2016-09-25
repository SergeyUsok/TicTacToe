namespace TicTacToe.Core.DataObjects
{
    public struct Cell
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Cell(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }
    }
}
