﻿using System;
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
        public static IPlayerViewModel GetPlayer(string playerName, Mark playersMark, IGame game)
        {
            if(playerName.Equals(Resources.HumanPlayer))
                return new HumanPlayerViewModel(playersMark);

            if(playerName.Equals(Resources.MinimaxPlayer))
                return new AiPlayerViewModel(new MiniMaxAiPlayer(game, playersMark, 10));

            if (playerName.Equals(Resources.OptimizedMinimaxPlayer))
                return new AiPlayerViewModel(new NeighborsLookingMiniMaxAiPlayer(game, playersMark, 10));

            if (playerName.Equals(Resources.IntuitiveAI))
                throw new NotImplementedException();

            if (playerName.Equals(Resources.DummyAI))
                throw new NotImplementedException();

            throw new NotSupportedException("Provided Player is not supported");
        }
    }
}