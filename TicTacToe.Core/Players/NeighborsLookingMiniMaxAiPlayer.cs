using System;
using System.Collections.Generic;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.Core.Players
{
    /// <summary>
    /// MiniMaxPlayer optimized for huge boards and average depth. It initially generates reduced number of board states
    /// Class looks only on empty cells that are neighbors to filled ones 
    /// </summary>
    public class NeighborsLookingMiniMaxAiPlayer : MiniMaxAiPlayer
    {
        public NeighborsLookingMiniMaxAiPlayer(IGame game, Mark myMark, int depth) 
            : base(game, myMark, depth)
        {
        }

        protected override bool CanBeProcessed(int x, int y, Mark[,] board)
        {
            var isEmpty = base.CanBeProcessed(x, y, board);

            return isEmpty && HasMarkedNeighbors(x, y, board);
        }

        private bool HasMarkedNeighbors(int x, int y, Mark[,] board)
        {
            for (int xAccretion = -1; xAccretion < 2; xAccretion++)
            {
                for (int yAccretion = -1; yAccretion < 2; yAccretion++)
                {
                    // skip current cell
                    if (xAccretion == 0 && yAccretion == 0)
                        continue;

                    var xNeighbor = x + xAccretion;
                    var yNeighbor = y + yAccretion;

                    var isMarked = board.WithinBounds(xNeighbor, yNeighbor) && 
                                   board[xNeighbor, yNeighbor] != Mark.Empty;

                    if (isMarked)
                        return true;
                }
            }

            return false;
        }
    }
}