using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.DataObjects
{
    public class GameEndedEventArgs : EventArgs
    {
        public MoveResult Result { get; private set; }
        public Player Winner { get; private set; }

        public GameEndedEventArgs(MoveResult result, Player winner)
        {
            Result = result;
            Winner = winner;
        }
    }
}
