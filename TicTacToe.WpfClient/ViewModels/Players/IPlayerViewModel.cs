using TicTacToe.Core.DataObjects;

namespace TicTacToe.ViewModels.Players
{
    interface IPlayerViewModel
    {
        void MakeMove(BoardViewModel board);

        Mark MyMark { get; }
    }
}
