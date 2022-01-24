using ComeX.WPF.Commands;
using ComeX.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ComeX.WPF.ViewModels {
    public class JoinServerViewModel : BaseViewModel {
        private string _url;
        public string Url {
            get {
                return _url;
            }
            set {
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        private LoginService _loginService;
        public string Username;

        private string _urlError;
        public string UrlError {
            get {
                return _urlError;
            }
            set {
                _urlError = value;
                OnPropertyChanged(nameof(UrlError));
            }
        }

        private Visibility _urlErrorVisibility;
        public Visibility UrlErrorVisibility {
            get {
                return _urlErrorVisibility;
            }
            set {
                _urlErrorVisibility = value;
                OnPropertyChanged(nameof(UrlErrorVisibility));
            }
        }

        private Brush _urlBoxBorder;
        public Brush UrlBoxBorder {
            get {
                return _urlBoxBorder;
            }
            set {
                _urlBoxBorder = value;
                OnPropertyChanged(nameof(UrlBoxBorder));
            }
        }

        private Brush _urlErrorColor;
        public Brush UrlErrorColor {
            get {
                return _urlErrorColor;
            }
            set {
                _urlErrorColor = value;
                OnPropertyChanged(nameof(UrlErrorColor));
            }
        }

        public ICommand JoinServerCommand { get; }

        public JoinServerViewModel(LoginService service, string username) {
            JoinServerCommand = new JoinServerCommand(this, service);
            _loginService = service;
            Username = username;
            UrlBoxBorder = (Brush)Application.Current.MainWindow.FindResource("DarkBlue3");
            UrlErrorColor = (Brush)Application.Current.MainWindow.FindResource("LightBlue1");
        }

        public void SetUrlErrorMessage(string errorMessage) {
            if (errorMessage != string.Empty) {
                UrlError = errorMessage;
                UrlErrorVisibility = Visibility.Visible;
            }
            UrlBoxBorder = Brushes.Red;
            UrlErrorColor = Brushes.Red;
        }

        public void SetUrlInfoMessage(string errorMessage) {
            if (errorMessage != string.Empty) {
                UrlError = errorMessage;
                UrlErrorVisibility = Visibility.Visible;
            }
            UrlErrorColor = (Brush)Application.Current.MainWindow.FindResource("LightBlue1");
            UrlBoxBorder = (Brush)Application.Current.MainWindow.FindResource("DarkBlue3");
        }

        public void UnsetUrlErrorMessage() {
            UrlErrorVisibility = Visibility.Collapsed;
            UrlBoxBorder = (Brush)Application.Current.MainWindow.FindResource("DarkBlue3");
        }
    }
}
