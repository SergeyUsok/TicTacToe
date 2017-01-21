using System;
using NUnit.Framework;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Tests
{
    [TestFixture]
    public class BoardExtensionsTest
    {
        [Test]
        public void WithinBounds_Test_Throws_Exception_On_Null_Board()
        {
            // Arrange
            Board board = null;
            const int x = 0;
            const int y = 0;

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => board.WithinBounds(x, y));
        }
        
        [Test]
        public void WithinBounds_Test_X_Is_Negative_Returns_False()
        {
            // Arrange
            var board = new Board(3, 3);
            const int x = -1;
            const int y = 2;

            // Act
            var result = board.WithinBounds(x, y);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void WithinBounds_Test_Y_Is_Negative_Returns_False()
        {
            // Arrange
            var board = new Board(3, 3);
            const int x = 0;
            const int y = -2;

            // Act
            var result = board.WithinBounds(x, y);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void WithinBounds_Test_X_Is_Greater_Than_Array_Length_Returns_False()
        {
            // Arrange
            var board = new Board(3, 4);
            const int x = 3;
            const int y = 0;

            // Act
            var result = board.WithinBounds(x, y);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void WithinBounds_Test_Y_Is_Greater_Than_Array_Length_Returns_False()
        {
            // Arrange
            var board = new Board(5, 3);
            const int x = 1;
            const int y = 4;

            // Act
            var result = board.WithinBounds(x, y);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void WithinBounds_Test_X_And_Y_Both_Outside_The_Bounds()
        {
            // Arrange
            var board = new Board(3, 4);
            const int x = -1;
            const int y = 4;

            // Act
            var result = board.WithinBounds(x, y);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void WithinBounds_Test_X_And_Y_Both_In_The_Bounds_Success()
        {
            // Arrange
            var board = new Board(2, 4);
            const int x = 0;
            const int y = 3;

            // Act
            var result = board.WithinBounds(x, y);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HorizontalInRow_Test_No_InRow_Check_Only_Filled_Cells()
        {
            // Arrange
            var board = new Board(3, 3);
            var move = Move.Make(1, 1, Mark.Cross);

            // Act
            var result = board.HorizontalInRow(move, currentMark => currentMark == move.Mark);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void HorizontalInRow_Test_Found_3_InRow()
        {
            // Arrange
            var board = new Board(3, 3);
            board[0, 0] = Mark.Cross;
            board[2, 0] = Mark.Cross;

            var move = Move.Make(1, 0, Mark.Cross);

            // Act
            var result = board.HorizontalInRow(move, currentMark => currentMark == move.Mark);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void HorizontalInRow_Test_No_InRow_Check_Both_Filled_And_Empty_Cells()
        {
            // Arrange
            var board = new Board(3, 3);
            var move = Move.Make(1, 1, Mark.Cross);

            // Act
            var result = board.HorizontalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void VerticalInRow_Test_No_InRow_Check_Only_Filled_Cells()
        {
            // Arrange
            var board = new Board(3, 3);
            var move = Move.Make(1, 1, Mark.Cross);

            // Act
            var result = board.VerticalInRow(move, currentMark => currentMark == move.Mark);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void VerticalInRow_Test_Found_3_InRow()
        {
            // Arrange
            var board = new Board(3, 3);
            board[0, 0] = Mark.Cross;
            board[0, 2] = Mark.Cross;

            var move = Move.Make(0, 1, Mark.Cross);

            // Act
            var result = board.VerticalInRow(move, currentMark => currentMark == move.Mark);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void VerticalInRow_Test_No_InRow_Check_Both_Filled_And_Empty_Cells()
        {
            // Arrange
            var board = new Board(3, 3);
            var move = Move.Make(1, 1, Mark.Cross);

            // Act
            var result = board.VerticalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void LeftDigonalInRow_Test_No_InRow_Check_Only_Filled_Cells()
        {
            // Arrange
            var board = new Board(3, 3);
            var move = Move.Make(1, 1, Mark.Cross);

            // Act
            var result = board.LeftDigonalInRow(move, currentMark => currentMark == move.Mark);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void LeftDigonalInRow_Test_Found_3_InRow()
        {
            // Arrange
            var board = new Board(3, 3);
            board[0, 0] = Mark.Cross;
            board[2, 2] = Mark.Cross;

            var move = Move.Make(1, 1, Mark.Cross);

            // Act
            var result = board.LeftDigonalInRow(move, currentMark => currentMark == move.Mark);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void LeftDigonalInRow_Test_No_InRow_Check_Both_Filled_And_Empty_Cells()
        {
            // Arrange
            var board = new Board(3, 3);
            var move = Move.Make(1, 1, Mark.Cross);

            // Act
            var result = board.LeftDigonalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void RightDiagonalInRow_Test_No_InRow_Check_Only_Filled_Cells()
        {
            // Arrange
            var board = new Board(3, 3);
            var move = Move.Make(1, 1, Mark.Cross);

            // Act
            var result = board.RightDiagonalInRow(move, currentMark => currentMark == move.Mark);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void RightDiagonalInRow_Test_Found_3_InRow()
        {
            // Arrange
            var board = new Board(3, 3);
            board[2, 0] = Mark.Cross;
            board[0, 2] = Mark.Cross;

            var move = Move.Make(1, 1, Mark.Cross);

            // Act
            var result = board.RightDiagonalInRow(move, currentMark => currentMark == move.Mark);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void RightDiagonalInRow_Test_No_InRow_Check_Both_Filled_And_Empty_Cells()
        {
            // Arrange
            var board = new Board(3, 3);
            var move = Move.Make(1, 1, Mark.Cross);

            // Act
            var result = board.RightDiagonalInRow(move, currentMark => currentMark == move.Mark || currentMark == Mark.Empty);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
        }
    }
}
