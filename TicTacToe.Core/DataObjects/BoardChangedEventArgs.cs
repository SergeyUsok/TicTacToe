using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core.DataObjects
{
    public class BoardChangedEventArgs : EventArgs
    {
        public Mark[,] Board { get; private set; }

        public BoardChangedEventArgs(Mark[,] board)
        {
            Board = board;
        }
    }
}
