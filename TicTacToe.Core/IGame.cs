using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core
{
    public interface IGame
    {
        Mark[,] Board { get; }

        GameSettings Settings { get; }

        MoveResult GetMoveResult(Movement movement);

        MoveResult GetMoveResult(Mark[,] board, Movement movement);
    }
}