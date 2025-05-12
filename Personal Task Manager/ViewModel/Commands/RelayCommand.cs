using System.Windows.Input;

namespace Personal_Task_Manager.ViewModel.Commands
{
    public class RelayCommand : ICommand
    {
        public Action<object> _execute;
        public Predicate<object>? _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object? canExecute = null)
        {
            return _canExecute == null || _canExecute(canExecute);
        }

        public void Execute(object? execute)
        {
            _execute(execute);
        }
    }

    public class RelayCommand<T> : ICommand
    {
        public Action<T> _execute;
        public Predicate<T>? _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object? canExecute = null)
        {
            return _canExecute == null || _canExecute((T)canExecute);
        }

        public void Execute(object? execute)
        {
            _execute((T)execute);
        }
    }
}
