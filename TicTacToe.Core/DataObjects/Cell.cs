namespace TicTacToe.Core.DataObjects
{
    public struct Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}";
        }
    }
}
