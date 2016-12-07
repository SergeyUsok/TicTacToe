using NUnit.Framework;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void GetMoveResult_Returns_KeepPlaying()
        {
            // Arrange
            var board = new Mark[3,3];
            board[0, 0] = Mark.Cross;
            board[2, 1] = Mark.Cross;
            board[1, 2] = Mark.Cross;
            board[2, 0] = Mark.Cross;

            var game = new Game(new GameSettings(3, 3, 3));

            //  Act
            var result = game.GetMoveResult(board, Move.Make(2, 0, Mark.Cross));
            
            // Assert
            Assert.AreEqual(GameState.KeepPlaying, result.GameState);
            Assert.IsNull(result.WinRow);
        }

        [Test]
        public void GetMoveResult_LeftDiagonal_Returns_Victory()
        {
            // Arrange
            var board = new Mark[3, 3];
            board[0, 0] = Mark.Cross;
            board[2, 2] = Mark.Cross;
            board[1, 1] = Mark.Cross;

            var game = new Game(new GameSettings(3, 3, 3));

            //  Act
            var result = game.GetMoveResult(board, Move.Make(1, 1, Mark.Cross));
            
            // Assert
            Assert.AreEqual(GameState.Victory, result.GameState);
            Assert.IsNotNull(result.WinRow);
            Assert.AreEqual(3, result.WinRow.Count);
        }

        [Test]
        public void GetMoveResult_RightDiagonal_Returns_Victory()
        {
            // Arrange
            var board = new Mark[3, 3];
            board[0, 2] = Mark.Cross;
            board[1, 1] = Mark.Cross;
            board[2, 0] = Mark.Cross;

            var game = new Game(new GameSettings(3, 3, 3));

            //  Act
            var result = game.GetMoveResult(board, Move.Make(2, 0, Mark.Cross));

            // Assert
            Assert.AreEqual(GameState.Victory, result.GameState);
            Assert.IsNotNull(result.WinRow);
            Assert.AreEqual(3, result.WinRow.Count);
        }

        [Test]
        public void GetMoveResult_Horizontal_WinsOnlyIfInRow()
        {
            // Arrange
            var board = new Mark[5, 5];
            board[0, 1] = Mark.Cross;
            board[0, 2] = Mark.Cross;
            board[0, 4] = Mark.Cross;

            var game = new Game(new GameSettings(3, 3, 3));

            //  Act
            var result = game.GetMoveResult(board, Move.Make(0, 4, Mark.Cross));

            // Assert
            Assert.AreEqual(GameState.KeepPlaying, result.GameState);
            Assert.IsNull(result.WinRow);
        }

        [Test]
        public void GetMoveResult_Horizontal_Victory()
        {
            // Arrange
            var board = new Mark[5, 5];
            board[0, 1] = Mark.Cross;
            board[0, 2] = Mark.Cross;
            board[0, 3] = Mark.Cross;

            var game = new Game(new GameSettings(3, 3, 3));

            //  Act
            var result = game.GetMoveResult(board, Move.Make(0, 3, Mark.Cross));

            // Assert
            Assert.AreEqual(GameState.Victory, result.GameState);
            Assert.IsNotNull(result.WinRow);
            Assert.AreEqual(3, result.WinRow.Count);
        }

        [Test]
        public void GetMoveResult_Vertical_WinsOnlyIfInRow()
        {
            // Arrange
            var board = new Mark[5, 5];
            board[0, 1] = Mark.Cross;
            board[1, 1] = Mark.Cross;
            board[4, 1] = Mark.Cross;

            var game = new Game(new GameSettings(3, 3, 3));

            //  Act
            var result = game.GetMoveResult(board, Move.Make(4, 1, Mark.Cross));

            // Assert
            Assert.AreEqual(GameState.KeepPlaying, result.GameState);
            Assert.IsNull(result.WinRow);
        }

        [Test]
        public void GetMoveResult_Vertical_Victory()
        {
            // Arrange
            var board = new Mark[5, 5];
            board[0, 1] = Mark.Cross;
            board[1, 1] = Mark.Cross;
            board[2, 1] = Mark.Cross;

            var game = new Game(new GameSettings(3, 3, 3));

            //  Act
            var result = game.GetMoveResult(board, Move.Make(2, 1, Mark.Cross));

            // Assert
            Assert.AreEqual(GameState.Victory, result.GameState);
            Assert.IsNotNull(result.WinRow);
            Assert.AreEqual(3, result.WinRow.Count);
        }

        [Test]
        public void GetMoveResult_Returns_Draw()
        {
            // Arrange
            var board = new Mark[3, 3];
            board[0, 1] = Mark.Cross;
            board[1, 1] = Mark.Nought;
            board[2, 1] = Mark.Nought;
            board[0, 0] = Mark.Cross;
            board[1, 0] = Mark.Cross;
            board[2, 0] = Mark.Nought;
            board[0, 2] = Mark.Cross;
            board[1, 2] = Mark.Nought;
            board[2, 2] = Mark.Cross;

            var game = new Game(new GameSettings(3, 3, 3));

            //  Act
            var result = game.GetMoveResult(board, Move.Make(2, 1, Mark.Nought));

            // Assert
            Assert.AreEqual(GameState.Draw, result.GameState);
            Assert.IsNull(result.WinRow);
        }

        [Test]
        public void GetMoveResult_Returns_KeepPlaying_OneMoveLeft()
        {
            // Arrange
            var board = new Mark[3, 3];
            board[0, 1] = Mark.Cross;
            board[1, 1] = Mark.Nought;
            board[2, 1] = Mark.Nought;
            board[0, 0] = Mark.Cross;
            board[2, 0] = Mark.Nought;
            board[0, 2] = Mark.Cross;
            board[1, 2] = Mark.Nought;
            board[2, 2] = Mark.Cross;

            var game = new Game(new GameSettings(3, 3, 3));

            //  Act
            var result = game.GetMoveResult(board, Move.Make(2, 1, Mark.Nought));

            // Assert
            Assert.AreEqual(GameState.KeepPlaying, result.GameState);
            Assert.IsNull(result.WinRow);
        }
    }
}
