using System;
using NUnit.Framework;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Tests
{
    [TestFixture]
    public class ExtensionsTest
    {
        [Test]
        public void WithinBounds_Test_Throws_Exception_On_Null_Board()
        {
            // Arrange
            Mark[,] board = null;
            const int x = 0;
            const int y = 0;

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => board.WithinBounds(x, y));
        }
        
        [Test]
        public void WithinBounds_Test_X_Is_Negative_Returns_False()
        {
            // Arrange
            var board = new Mark[3, 3];
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
            var board = new Mark[3, 3];
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
            var board = new Mark[3, 4];
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
            var board = new Mark[5, 3];
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
            var board = new Mark[3, 4];
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
            var board = new Mark[2, 4];
            const int x = 0;
            const int y = 3;

            // Act
            var result = board.WithinBounds(x, y);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmpty_Test_Throws_Exception_On_Null_Board()
        {
            // Arrange
            Mark[,] board = null;

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => board.IsEmpty());
        }

        [Test]
        public void IsEmpty_Test_Returns_True_On_Empty_Board()
        {
            // Arrange
            var board = new Mark[3,3];

            // Act
            var result = board.IsEmpty();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmpty_Test_Returns_False_On_NonEmpty_Board()
        {
            // Arrange
            var board = new Mark[3, 3];
            board[2,2] = Mark.Cross;

            // Act
            var result = board.IsEmpty();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
