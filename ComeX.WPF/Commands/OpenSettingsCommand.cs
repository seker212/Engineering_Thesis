using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using ComeX.WPF.Views;
using ComeX.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class OpenSettingsCommand : ICommand {
        private readonly ChatViewModel _viewModel;
        private readonly LoginService _service;
        private SettingsView _settingsView;

        public OpenSettingsCommand(ChatViewModel viewModel, LoginService service) {
            _viewModel = viewModel;
            _service = service;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                OpenSettingsWindow();

            } catch (ArgumentException e) {
                _viewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Unable to create survey.";
            }
        }

        private void OpenSettingsWindow() {
            _settingsView = new SettingsView();
            _settingsView.DataContext = new SettingsViewModel(_service, _viewModel.LoginDM);
            _settingsView.Owner = Application.Current.MainWindow;
            _settingsView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Application.Current.MainWindow.IsEnabled = false;

            _settingsView.Show();
        }
    }
}
