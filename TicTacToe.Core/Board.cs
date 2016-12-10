using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core
{
    public class Board
    {
        private readonly Mark[,] _board;
        
        internal Board(int width, int height)
        {
            Width = width;
            Height = height;
            // .NET array uses 1st dimension as a number of rows (height)
            // .NET array uses 2st dimension as a number of columns (width)
            _board = new Mark[height, width]; 
        }

        // For test purposes
        internal Board(Mark[,] board)
        {
            Width = board.GetLength(1);
            Height = board.GetLength(0);
            _board = board;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Mark this[int x, int y]
        {
            get
            {
                return _board[y, x];
            }
            set
            {
                _board[y, x] = value;
            }
        }

        // TODO: optimize by counting number of empty cells during game
        // instead of walking through all cells every time
        public bool IsEmpty
        {
            get
            {
                for (int i = 0; i < _board.GetLength(0); i++)
                {
                    for (int j = 0; j < _board.GetLength(1); j++)
                    {
                        if (_board[i, j] != Mark.Empty)
                            return false;
                    }
                }

                return true;
            }
        }
    }
}
