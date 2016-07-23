using NUnit.Framework;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Tests
{
    [TestFixture]
    public class GameStateCheckerTests
    {
        [Test]
        public void GetMoveResult_Returns_KeepPlaying()
        {
            // Arrange
            const int numberInRow = 3;
            var board = new Mark[3,3];
            board[0, 0] = Mark.Cross;
            board[2, 1] = Mark.Cross;
            board[1, 2] = Mark.Cross;
            board[2, 0] = Mark.Cross;

            //  Act
            var result = GameStateChecker.GetMoveResult(board, Movement.Make(2, 0, Mark.Cross), numberInRow);
            
            // Assert
            Assert.AreEqual(MoveResult.KeepPlaying, result);
        }

        [Test]
        public void GetMoveResult_LeftDiagonal_Returns_Victory()
        {
            // Arrange
            const int numberInRow = 3;
            var board = new Mark[3, 3];
            board[0, 0] = Mark.Cross;
            board[2, 2] = Mark.Cross;
            board[1, 1] = Mark.Cross;

            //  Act
            var result = GameStateChecker.GetMoveResult(board, Movement.Make(1, 1, Mark.Cross), numberInRow);
            
            // Assert
            Assert.AreEqual(MoveResult.Victory, result);
        }

        [Test]
        public void GetMoveResult_RightDiagonal_Returns_Victory()
        {
            // Arrange
            const int numberInRow = 3;
            var board = new Mark[3, 3];
            board[0, 2] = Mark.Cross;
            board[1, 1] = Mark.Cross;
            board[2, 0] = Mark.Cross;

            //  Act
            var result = GameStateChecker.GetMoveResult(board, Movement.Make(2, 0, Mark.Cross), numberInRow);

            // Assert
            Assert.AreEqual(MoveResult.Victory, result);
        }

        [Test]
        public void GetMoveResult_Horizontal_WinsOnlyIfInRow()
        {
            // Arrange
            const int numberInRow = 3;
            var board = new Mark[5, 5];
            board[0, 1] = Mark.Cross;
            board[0, 2] = Mark.Cross;
            board[0, 4] = Mark.Cross;

            //  Act
            var result = GameStateChecker.GetMoveResult(board, Movement.Make(0, 4, Mark.Cross), numberInRow);

            // Assert
            Assert.AreEqual(MoveResult.KeepPlaying, result);
        }

        [Test]
        public void GetMoveResult_Horizontal_Victory()
        {
            // Arrange
            const int numberInRow = 3;
            var board = new Mark[5, 5];
            board[0, 1] = Mark.Cross;
            board[0, 2] = Mark.Cross;
            board[0, 3] = Mark.Cross;

            //  Act
            var result = GameStateChecker.GetMoveResult(board, Movement.Make(0, 3, Mark.Cross), numberInRow);

            // Assert
            Assert.AreEqual(MoveResult.Victory, result);
        }

        [Test]
        public void GetMoveResult_Vertical_WinsOnlyIfInRow()
        {
            // Arrange
            const int numberInRow = 3;
            var board = new Mark[5, 5];
            board[0, 1] = Mark.Cross;
            board[1, 1] = Mark.Cross;
            board[4, 1] = Mark.Cross;

            //  Act
            var result = GameStateChecker.GetMoveResult(board, Movement.Make(4, 1, Mark.Cross), numberInRow);

            // Assert
            Assert.AreEqual(MoveResult.KeepPlaying, result);
        }

        [Test]
        public void GetMoveResult_Vertical_Victory()
        {
            // Arrange
            const int numberInRow = 3;
            var board = new Mark[5, 5];
            board[0, 1] = Mark.Cross;
            board[1, 1] = Mark.Cross;
            board[2, 1] = Mark.Cross;

            //  Act
            var result = GameStateChecker.GetMoveResult(board, Movement.Make(2, 1, Mark.Cross), numberInRow);

            // Assert
            Assert.AreEqual(MoveResult.Victory, result);
        }

        [Test]
        public void GetMoveResult_Returns_Draw()
        {
            // Arrange
            const int numberInRow = 3;
            var board = new Mark[3, 3];
            board[0, 1] = Mark.Cross;
            board[1, 1] = Mark.Zero;
            board[2, 1] = Mark.Zero;
            board[0, 0] = Mark.Cross;
            board[1, 0] = Mark.Cross;
            board[2, 0] = Mark.Zero;
            board[0, 2] = Mark.Cross;
            board[1, 2] = Mark.Zero;
            board[2, 2] = Mark.Cross;

            //  Act
            var result = GameStateChecker.GetMoveResult(board, Movement.Make(2, 1, Mark.Zero), numberInRow);

            // Assert
            Assert.AreEqual(MoveResult.Draw, result);
        }

        [Test]
        public void GetMoveResult_Returns_KeepPlaying_OneMoveLeft()
        {
            // Arrange
            const int numberInRow = 3;
            var board = new Mark[3, 3];
            board[0, 1] = Mark.Cross;
            board[1, 1] = Mark.Zero;
            board[2, 1] = Mark.Zero;
            board[0, 0] = Mark.Cross;
            board[2, 0] = Mark.Zero;
            board[0, 2] = Mark.Cross;
            board[1, 2] = Mark.Zero;
            board[2, 2] = Mark.Cross;

            //  Act
            var result = GameStateChecker.GetMoveResult(board, Movement.Make(2, 1, Mark.Zero), numberInRow);

            // Assert
            Assert.AreEqual(MoveResult.KeepPlaying, result);
        }
    }
}
