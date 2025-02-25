﻿using System;
using System.Windows.Input;

namespace CommercialRecords.Common
{
    class ICommandImp : ICommand
    {
        private readonly Action<object> _execute = null;
        private readonly Predicate<object> _canExecute = null;
        private bool canExecute = true;
        public event EventHandler CanExecuteChanged;

        #region Constructors
        public ICommandImp(Action<object> execute) : this(execute, null)
        { 
            // deliberately empty 
        }

        public ICommandImp(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }
            else if (parameter != null && 
                     parameter.GetType().Equals(typeof(bool)))
            {
                canExecute = (bool)parameter;
            }
            return canExecute;
        }

        public void Execute(object parameter)
        {
            if ((null != parameter && parameter is string && "ignore_session_control" == (string)parameter) && null != _execute)
            {
                _execute(parameter);
                return;
            }

            CrsAuthentication authInstance = CrsAuthentication.getInstance();

            if (!authInstance.SessionControl.SessionStatus.Equals(CrsAuthentication.SESSION_STATUS.TIME_OUT))
            {
                authInstance.updateTimeoutDate();

                if (_execute != null)
                    _execute(parameter);
            }
            else
            {
                authInstance.showAuthentication();
            }
        }
    }
}
