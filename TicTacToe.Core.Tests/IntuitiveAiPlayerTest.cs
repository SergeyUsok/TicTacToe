using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core.DataObjects;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Tests
{
    [TestFixture]
    public class IntuitiveAiPlayerTest
    {
        [Test]
        public void MakeMove_Should_Block_Opponents_Victory()
        {
            // Arrange
            var game = GetGame();
            var player = new IntuitiveAiPlayer(game, Mark.Nought);

            var board = new Mark[,]
                {
                    {Mark.Nought, Mark.Empty, Mark.Cross},
                    {Mark.Empty, Mark.Cross, Mark.Empty},
                    {Mark.Empty,  Mark.Empty,  Mark.Empty}
                };

            game.Board = new Board(board);

            // Arrays can have more than one dimension. For example, the following declaration creates a two-dimensional array of four rows and two columns.
            var a = game.Board[0, 2];

            // Act
            var actualMove = player.MakeMove();

            // Assert
            Assert.IsTrue(actualMove.X == 0 && actualMove.Y == 2);
        }

        private Game GetGame()
        {
            return new Game(new GameSettings(3, 3, 3));
        }
    }
}
