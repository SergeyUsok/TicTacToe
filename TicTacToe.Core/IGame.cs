using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core
{
    public interface IGame
    {
        Board Board { get; }

        GameSettings Settings { get; }

        MoveResult GetMoveResult(Move movement);

        MoveResult GetMoveResult(Board board, Move movement);
    }
}