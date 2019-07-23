using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhantomLibSamples.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private bool _isValidating;

        private bool _showErrorPassword;
        public bool ShowErrorPassword
        {
            get => _showErrorPassword;
            set
            {
                _showErrorPassword = value;
                OnPropertyChanged(nameof(ShowErrorPassword));
            }
        }

        private string _passwordText;
        public string PasswordText
        {
            get => _passwordText;
            set
            {
                ShowErrorPassword = _isValidating && !PasswordValidator(value);
                _passwordText = value;
                OnPropertyChanged(nameof(PasswordText));
            }
        }

        public ICommand LoginCommand { get; private set; }

        public MainPageViewModel()
        {
            LoginCommand = new Command(LoginCommandExecute);
        }

        private void LoginCommandExecute()
        {
            _isValidating = true;
            ShowErrorPassword = !PasswordValidator(PasswordText);
        }

        private bool PasswordValidator(string password) => !string.IsNullOrEmpty(password);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}