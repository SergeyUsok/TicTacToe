using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.ViewModels.Events
{
    class MoveEvent
    {
        public Movement Movement { get; private set; }

        public MoveEvent(Movement movement)
        {
            Movement = movement;
        }
    }
}
