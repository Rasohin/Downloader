using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MusicAndBooksDownloader.MVVMEvents
{
    class ButtonClickEvent : ICommand
    {
        private Action<object> _BClick;
        private Func<object, bool> _CanBClick;
        public event EventHandler CanExecuteChanged;

        public ButtonClickEvent(Action<object> BClick, Func<object, bool> CanBClick = null)
        {
            _BClick = BClick;
            _CanBClick = CanBClick;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null)
                return true;
            return _CanBClick == null || _CanBClick(parameter);
        }

        public void Execute(object parameter)
        {
            _BClick(parameter);
        }
    }
}
