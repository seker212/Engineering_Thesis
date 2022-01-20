using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using ComeX.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class OpenJoinServerCommand : ICommand {
        private readonly ChatViewModel _viewModel;
        private readonly LoginService _service;
        private JoinServerWindow _joinServerWindow;

        public OpenJoinServerCommand(ChatViewModel viewModel, LoginService service) {
            _viewModel = viewModel;
            _service = service;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                OpenJoinServerWindow();

            } catch (ArgumentException e) {
                _viewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Unable to create survey.";
            }
        }

        private void OpenJoinServerWindow() {
            _joinServerWindow = new JoinServerWindow();
            _joinServerWindow.DataContext = new JoinServerViewModel(_service, _viewModel.LoginDM.Username);
            _joinServerWindow.Owner = Application.Current.MainWindow;
            _joinServerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _joinServerWindow.Show();
        }
    }
}
