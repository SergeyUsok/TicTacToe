using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.ViewModels.Players
{
    interface IPlayerViewModel
    {
        Task<Move> MakeMoveAsync(BoardViewModel board);

        Mark MyMark { get; }
    }
}
