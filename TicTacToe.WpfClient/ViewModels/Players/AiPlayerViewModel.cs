using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;
using TicTacToe.Core.Players;
using TicTacToe.ViewModels.Events;

namespace TicTacToe.ViewModels.Players
{
    class AiPlayerViewModel : IPlayerViewModel
    {
        private readonly AiPlayer _aiPlayer;

        public AiPlayerViewModel(AiPlayer aiPlayer)
        {
            _aiPlayer = aiPlayer;
        }

        public async Task<Move> MakeMoveAsync(BoardViewModel board)
        {
            board.IsActive = false; // make board inactive for user's input

            return await Task.Run(() => _aiPlayer.MakeMove());
        }

        public Mark MyMark
        {
            get
            {
                return _aiPlayer.MyMark;
            }
        }
    }
}
