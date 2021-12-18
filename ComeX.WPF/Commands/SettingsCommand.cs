using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using ComeX.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class SettingsCommand : ICommand {
        private readonly ChatViewModel _viewModel;
        private readonly ChatService _service;

        public SettingsCommand(ChatViewModel viewModel, ChatService service) {
            _viewModel = viewModel;
            _service = service;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                OptionEnum option = OpenSettingsWindow();
                switch (option) {
                    case OptionEnum.Logout: {
                            LogoutOption();
                            break;
                        }
                    case OptionEnum.ChangePassword: {
                            ChangePasswordOption();
                            break;
                        }
                    case OptionEnum.DeleteAccount: {
                            DeleteAccountOption();
                            break;
                        }
                    case OptionEnum.Cancel: {
                            break;
                        }
                } 
            } catch (ArgumentException e) {
                _viewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Unable to create survey.";
            }
        }

        private OptionEnum OpenSettingsWindow() {
            SettingsWindow settingsWindow = new SettingsWindow();
            OptionEnum option;

            settingsWindow.Owner = Application.Current.MainWindow;
            settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Application.Current.MainWindow.IsEnabled = false;

            settingsWindow.ShowDialog();
            option = settingsWindow.Option;

            return option;
        }

        private void LogoutOption() {
            _viewModel.ChangeViewToLoginCommand.Execute(null);
        }

        private void ChangePasswordOption() {
            // todo
        }

        private void DeleteAccountOption() {
            // todo
        }
    }
}
