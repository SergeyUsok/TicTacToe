using System.Collections.Generic;

namespace TicTacToe.Core.DataObjects
{
    public class MoveResult
    {
        public GameState GameState { get; internal set; }

        public Mark Mark { get; internal set; }

        public List<Position> WinRow { get; internal set; }  
    }
}
