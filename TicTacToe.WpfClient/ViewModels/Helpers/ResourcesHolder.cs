using System.Collections.Generic;
using TicTacToe.Core.DataObjects;
using TicTacToe.Properties;

namespace TicTacToe.ViewModels.Helpers
{
    static class ResourcesHolder
    {
        public static string Cross
        {
            get { return Resources.Cross; }
        }

        public static string Nought
        {
            get { return Resources.Nought; }
        }

        public static string Void
        {
            get
            {
                return string.Empty;
            }
        }

        private static IReadOnlyCollection<string> _players;
        public static IReadOnlyCollection<string> Players
        {
            get
            {
                if(_players == null)
                    _players = new List<string>
                        {
                            Resources.HumanPlayer,
                            Resources.MinimaxPlayer,
                            Resources.OptimizedMinimaxPlayer,
                            Resources.IntuitiveAI,
                            Resources.DummyAI
                        }.AsReadOnly();

                return _players;
            }
        }

        private static IReadOnlyCollection<string> _boards;
        public static IReadOnlyCollection<string> Boards
        {
            get
            {
                if (_boards == null)
                    _boards = new List<string>
                        {
                            Resources.Board3x3With3inRow,
                            Resources.Board5x5With5inRow,
                            Resources.Board10x10With5inRow
                        }.AsReadOnly();

                return _boards;
            }
        }

        public static string HumanSymbol
        {
            get { return Resources.HumanPlayer; }
        }

        public static string MiniMaxPlayer
        {
            get { return Resources.MinimaxPlayer; }
        }

        public static string OptimizedMinimaxPlayer
        {
            get { return Resources.OptimizedMinimaxPlayer; }
        }

        public static string GetMoveMessage(Mark mark)
        {
            return string.Format(Resources.MoveMessage, mark.AsString());
        }

        public static string GetTurnMessage(Mark mark)
        {
            return string.Format(Resources.TurnMessage, mark.AsString());
        }

        public static string GetWinMessage(Mark mark)
        {
            return string.Format(Resources.WinMessage, mark.AsString());
        }

        public static string GetDrawMessage()
        {
            return Resources.DrawMessage;
        }
    }
}
