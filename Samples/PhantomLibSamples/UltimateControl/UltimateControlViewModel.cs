using System;
using System.Windows.Input;
using PhantomLib.Models;
using Xamarin.Forms;

namespace PhantomLibSamples.UltimateControl
{
    public class UltimateControlViewModel : BaseAttachable
    {
        private bool _isValidating;

        public ICommand LoginCommand { get; private set; }

        private bool _showErrorPassword;
        public bool ShowErrorPassword
        {
            get => _showErrorPassword;
            set => SetProperty(ref _showErrorPassword, value);
        }

        private string _ultimateControlText;
        public string UltimateControlText
        {
            get { return _ultimateControlText; }
            set
            {
                SetProperty(ref _ultimateControlText, value);
                ValidateText(value);
            }
        }

        private string _passwordText;
        public string PasswordText
        {
            get => _passwordText;
            set
            {
                ShowErrorPassword = _isValidating && !PasswordValidator(value);
                SetProperty(ref _passwordText, value);
            }
        }

        private bool _isUltimateError;
        public bool IsUltimateError
        {
            get { return _isUltimateError; }
            set { SetProperty(ref _isUltimateError, value); }
        }

        private void ValidateText(string text)
        {
            IsUltimateError = text?.Length > 10 ? true : false;
        }

        public UltimateControlViewModel()
        {
            LoginCommand = new Command(LoginCommandExecute);
        }

        private void LoginCommandExecute()
        {
            _isValidating = true;
            ShowErrorPassword = !PasswordValidator(PasswordText);
        }

        private bool PasswordValidator(string password) => !string.IsNullOrEmpty(password);
    }
}
