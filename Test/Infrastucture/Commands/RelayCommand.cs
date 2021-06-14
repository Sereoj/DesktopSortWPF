using System;
using Test.Infrastucture.Commands.Base;

namespace Test.Infrastucture.Commands
{
    internal class RelayCommand : Command
    {
        private readonly Func<object, bool> _CanExecute;
        private readonly Action<object> _Execute;

        public RelayCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _CanExecute?.Invoke(parameter) ?? true;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            _Execute(parameter);
        }
    }
}