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

            game.Board = new Mark[,]
                {
                    {Mark.Nought, Mark.Empty, Mark.Cross},
                    {Mark.Empty, Mark.Cross, Mark.Empty},
                    {Mark.Empty,  Mark.Empty,  Mark.Empty}
                };

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
