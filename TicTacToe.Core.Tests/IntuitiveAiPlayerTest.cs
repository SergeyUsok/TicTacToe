using NUnit.Framework;
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
                    {Mark.Cross, Mark.Empty, Mark.Empty},
                    {Mark.Empty, Mark.Cross, Mark.Empty},
                    {Mark.Nought,  Mark.Empty,  Mark.Empty}
                };

            game.Board = new Board(board);

            // Act
            var actualMove = player.MakeMove();

            // Assert
            Assert.IsTrue(actualMove.X == 2 && actualMove.Y == 2);
        }

        [Test]
        public void MakeMove_Test_Should_Choose_Victory_Move()
        {
            // Arrange
            var game = GetGame();
            var player = new IntuitiveAiPlayer(game, Mark.Nought);

            var board = new Mark[,]
                {
                    {Mark.Empty, Mark.Cross, Mark.Cross},
                    {Mark.Empty, Mark.Cross, Mark.Empty},
                    {Mark.Nought,  Mark.Nought,  Mark.Empty}
                };

            game.Board = new Board(board);

            // Act
            var actualMove = player.MakeMove();

            // Assert
            Assert.IsTrue(actualMove.X == 2 && actualMove.Y == 2);
        }

        [Test]
        public void MakeMove_Should_Seek_For_Maximum_InRow_Plays_For_Nought()
        {
            // Arrange
            var game = GetGame(4, 4, 4);
            var player = new IntuitiveAiPlayer(game, Mark.Nought);

            var board = new Mark[,]
                {
                    {Mark.Cross, Mark.Cross, Mark.Empty, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty},
                    {Mark.Nought, Mark.Empty, Mark.Nought, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty}
                };

            game.Board = new Board(board);

            // Act
            var actualMove = player.MakeMove();

            // Assert
            Assert.IsTrue(actualMove.X == 1 && actualMove.Y == 2);
        }

        [Test]
        public void MakeMove_Should_Seek_For_Maximum_InRow_Plays_For_Cross()
        {
            // Arrange
            var game = GetGame(4, 4, 4);
            var player = new IntuitiveAiPlayer(game, Mark.Cross);

            var board = new Mark[,]
                {
                    {Mark.Cross, Mark.Cross, Mark.Empty, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty},
                    {Mark.Nought, Mark.Empty, Mark.Nought, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty}
                };

            game.Board = new Board(board);

            // Act
            var actualMove = player.MakeMove();

            // Assert
            Assert.IsTrue(actualMove.X == 2 && actualMove.Y == 0);
        }

        [Test]
        public void MakeMove_Big_Board_Should_Block_Opponents_Move()
        {
            // Arrange
            var game = GetGame(6, 6, 4);
            var player = new IntuitiveAiPlayer(game, Mark.Nought);

            var board = new Mark[,]
                {
                    {Mark.Nought, Mark.Nought, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Cross, Mark.Cross, Mark.Empty, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty}
                };

            game.Board = new Board(board);

            // Act
            var actualMove = player.MakeMove();

            // Assert
            Assert.IsTrue(actualMove.X == 1 && actualMove.Y == 3);
        }

        private Game GetGame(int width = 3, int height = 3, int inRow = 3)
        {
            return new Game(new GameSettings(width, height, inRow));
        }
    }
}
