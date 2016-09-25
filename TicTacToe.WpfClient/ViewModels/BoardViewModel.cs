using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.ViewModels
{
    class BoardViewModel : ViewModelBase
    {
        private readonly Mark[,] _board;
        private IList<TileViewModel> _tiles;
        private bool _isActive;

        public BoardViewModel(Mark[,] board)
        {
            _board = board;
            Tiles = GetTiles(board);
            Rows = board.GetLength(0);
            Columns = board.GetLength(1);
        }

        private IList<TileViewModel> GetTiles(Mark[,] board)
        {
            var boardView = new List<TileViewModel>();

            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    boardView.Add(new TileViewModel(x, y));
                }
            }

            return boardView;
        }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public IList<TileViewModel> Tiles
        {
            get { return _tiles; }
            set
            {
                if (Equals(value, _tiles)) return;
                _tiles = value;
                OnPropertyChanged();
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (value.Equals(_isActive)) return;
                _isActive = value;
                OnPropertyChanged();
            }
        }

        public void SetBoardCell(int x, int y, Mark mark)
        {
            _board[x, y] = mark;
            var tile = _tiles.Single(t => t.X == x && t.Y == y);
            tile.Value = mark;
        }

        public void HighlightWinRow(List<Cell> winRow)
        {
            winRow.ForEach(cell => Tiles.Single(t => t.X == cell.X && t.Y == cell.Y)
                                        .IsWinTile = true);
        }
    }
}
