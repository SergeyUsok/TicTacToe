using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TicTacToe.Annotations;

namespace TicTacToe
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _predicate;

        public RelayCommand(Action<object> action)
            : this(action, param => true)
        {
        }

        public RelayCommand([NotNull] Action<object> action, [NotNull] Func<object, bool> predicate)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (predicate == null) throw new ArgumentNullException("predicate");

            _action = action;
            _predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            return _predicate(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
