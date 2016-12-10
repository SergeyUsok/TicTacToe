using NUnit.Framework;
using TicTacToe.Core.DataObjects;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Tests
{
    [TestFixture]
    public class MiniMaxPlayerTest
    {
        [Test]
        public void MakeMove_Test_Should_Block_Opponents_Win()
        {
            // Arrange
            const int depth = 6;
            var game = GetGame();
            var player = new MiniMaxAiPlayer(game, Mark.Nought, depth);

            var board = new Mark[,]
                {
                    {Mark.Empty, Mark.Cross, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Cross},
                    {Mark.Nought,  Mark.Nought,  Mark.Cross}
                };

            game.Board = new Board(board);

            // Act
            var actualMove = player.MakeMove();

            // Assert
            Assert.IsTrue(actualMove.X == 0 && actualMove.Y == 2);
        }

        [Test]
        public void MakeMove_Test_Should_Choose_Win_Move()
        {
            // Arrange
            const int depth = 6;
            var game = GetGame();
            var player = new MiniMaxAiPlayer(game, Mark.Nought, depth);

            var board = new Mark[,]
                {
                    {Mark.Empty, Mark.Cross, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Cross},
                    {Mark.Nought,  Mark.Nought,  Mark.Empty}
                };

            game.Board = new Board(board);

            // Act
            var actualMove = player.MakeMove();

            // Assert
            Assert.IsTrue(actualMove.X == 2 && actualMove.Y == 2);
        }

        [Test]
        public void MakeMove_Test_Choose_First_Possible_Move_If_All_Moves_Lead_To_Lost()
        {
            // Arrange
            const int depth = 6;
            var game = GetGame();
            var player = new MiniMaxAiPlayer(game, Mark.Nought, depth);

            var board = new Mark[,]
                {
                    {Mark.Nought, Mark.Empty, Mark.Cross}, // will choose [0,1] because any move
                    {Mark.Empty, Mark.Cross, Mark.Cross}, // leads to lost
                    {Mark.Empty,  Mark.Nought,  Mark.Empty} 
                };

            game.Board = new Board(board);

            // Act
            var actualMove = player.MakeMove();

            // Assert
            Assert.IsTrue(actualMove.X == 0 && actualMove.Y == 1);
        }

        private Game GetGame()
        {
            return new Game(new GameSettings(3, 3, 3));
        }
    }
}
