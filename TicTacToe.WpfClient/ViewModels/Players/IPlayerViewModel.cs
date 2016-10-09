using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.ViewModels.Players
{
    interface IPlayerViewModel
    {
        Task<Movement> MakeMoveAsync(BoardViewModel board);

        Mark MyMark { get; }
    }
}
