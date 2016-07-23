using NUnit.Framework;
using TicTacToe.Core.DataObjects;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Tests
{
    [TestFixture]
    public class MiniMaxPlayerTest
    {
        [Test]
        public void MakeMove_Test()
        {
            // Arrange
            const int depth = 6;
            var player = new MiniMaxPlayer(GetParameters(), Mark.Zero, depth);

            var board = new Mark[,]
                {
                    {Mark.Empty, Mark.Cross, Mark.Empty},
                    {Mark.Empty, Mark.Empty, Mark.Cross},
                    {Mark.Zero,  Mark.Zero,  Mark.Cross}
                };

            // Act
            Movement actualMove = null;
            player.Move += (o, args) => actualMove = args.Movement;
            
            player.MakeMove(board);
            
            // Assert
            Assert.IsTrue(actualMove.X == 0 && actualMove.Y == 2);
        }

        private GameParameters GetParameters()
        {
            return new GameParameters(3, 3, 3);
        }
    }
}
