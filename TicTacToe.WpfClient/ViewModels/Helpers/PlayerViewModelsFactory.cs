using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core;
using TicTacToe.Core.DataObjects;
using TicTacToe.Core.Players;
using TicTacToe.Properties;
using TicTacToe.ViewModels.Players;

namespace TicTacToe.ViewModels.Helpers
{
    class PlayerViewModelsFactory
    {
        public static IPlayerViewModel GetPlayer(string playerName, Mark playersMark, IGame game, int depth)
        {
            if(playerName.Equals(Resources.HumanPlayer))
                return new HumanPlayerViewModel(playersMark);

            if(playerName.Equals(Resources.MinimaxPlayer))
                return new AiPlayerViewModel(new MiniMaxAiPlayer(game, playersMark, depth));

            if (playerName.Equals(Resources.OptimizedMinimaxPlayer))
                return new AiPlayerViewModel(new NeighborsLookingMiniMaxAiPlayer(game, playersMark, depth));

            if (playerName.Equals(Resources.ForkableAI))
                return new AiPlayerViewModel(new ForkableAiPlayer(game, playersMark));

            if (playerName.Equals(Resources.HumanLikeAI))
                return new AiPlayerViewModel(new HumanLikeAiPlayer(game, playersMark));

            if (playerName.Equals(Resources.DummyAI))
                return new AiPlayerViewModel(new StupidRandomAiPlayer(game, playersMark));

            throw new NotSupportedException("Provided Player is not supported");
        }
    }
}
