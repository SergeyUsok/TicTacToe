using TicTacToe.Core;
using TicTacToe.ViewModels.Players;

namespace TicTacToe.ViewModels.Events
{
    class GameStartedEvent
    {
        public GameStartedEvent(IGame game, IPlayerViewModel playerX, IPlayerViewModel playerO)
        {
            Game = game;
            PlayerX = playerX;
            PlayerO = playerO;
        }

        public IPlayerViewModel PlayerX { get; private set; }
        public IPlayerViewModel PlayerO { get; private set; }
        public IGame Game { get; private set; }
    }
}
