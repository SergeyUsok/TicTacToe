using System;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Players
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(GameParameters parameters, Mark myMark) 
            : base(parameters, myMark)
        {

        }

        public event EventHandler<BoardChangedEventArgs> AllowMovement;

        public override void MakeMove(Mark[,] board)
        {
            OnAllowMovement(new BoardChangedEventArgs(board));
        }

        public void MakeMove(Movement movement)
        {
            OnMove(new MoveEventArgs(movement));
        }

        private void OnAllowMovement(BoardChangedEventArgs e)
        {
            var handler = AllowMovement;
            if (handler != null) handler(this, e);
        }
    }
}