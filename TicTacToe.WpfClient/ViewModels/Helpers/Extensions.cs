using System;
using System.Collections.ObjectModel;
using TicTacToe.Core.DataObjects;

namespace TicTacToe.ViewModels.Helpers
{
    static class Extensions
    {
        public static string AsString(this Mark source)
        {
            switch (source)
            {
                case Mark.Cross:
                    return "X";
                case Mark.Empty:
                    return string.Empty;
                case Mark.Nought:
                    return "O";
                default:
                    throw new NotSupportedException("Provided Mark not supported");
            }
        }

        public static void Push<T>(this ObservableCollection<T> asStack, T item)
        {
            asStack.Insert(0, item);
        }
    }
}
