using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core
{
    public interface IGame
    {
        Mark[,] Board { get; }

        GameSettings Settings { get; }

        MoveResult GetMoveResult(Move movement);

        MoveResult GetMoveResult(Mark[,] board, Move movement);
    }
}