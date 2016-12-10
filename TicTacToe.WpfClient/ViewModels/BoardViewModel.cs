using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.ViewModels
{
    class BoardViewModel : ViewModelBase
    {
        private readonly Board _board;
        private IList<TileViewModel> _tiles;
        private bool _isActive;

        public BoardViewModel(Board board)
        {
            _board = board;
            Tiles = GetTiles(board);
            Rows = board.Height;
            Columns = board.Width;
        }

        private IList<TileViewModel> GetTiles(Board board)
        {
            var boardView = new List<TileViewModel>();

            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
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

        public void HighlightWinRow(List<Position> winRow)
        {
            winRow.ForEach(cell => Tiles.Single(t => t.X == cell.X && t.Y == cell.Y)
                                        .IsWinTile = true);
        }
    }
}
