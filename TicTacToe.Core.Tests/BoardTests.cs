using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void IsEmpty_Test_Returns_True()
        {
            // Arrange
            var board = new Board(3, 3);

            // Act
            var result = board.IsEmpty;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsEmpty_Test_Returns_False()
        {
            // Arrange
            var board = new Board(3, 3);
            board[1, 1] = Mark.Cross;

            // Act
            var result = board.IsEmpty;

            // Assert
            Assert.IsFalse(result);
        }
    }
}
