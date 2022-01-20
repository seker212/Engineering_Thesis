using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    class JoinServerCommand : ICommand {
        private readonly JoinServerViewModel _joinServerViewModel;
        private readonly LoginService _loginService;

        public JoinServerCommand(JoinServerViewModel joinServerViewModel, LoginService loginService) {
            _joinServerViewModel = joinServerViewModel;
            _loginService = loginService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                _joinServerViewModel.UnsetUrlErrorMessage();

                bool isInputCorrect = true;
                string url = _joinServerViewModel.Url;

                if (string.IsNullOrWhiteSpace(url)) {
                    _joinServerViewModel.SetUrlErrorMessage("Server address cannot be empty");
                    isInputCorrect = false;
                }
                if (isInputCorrect) {
                    bool result = await _loginService.JoinServer(_joinServerViewModel.Username, url);
                    if (result)
                        _joinServerViewModel.SetUrlInfoMessage("Joined server successfully");
                }
            } catch (Exception e) {
                _joinServerViewModel.SetUrlErrorMessage("Couldn't join server");
            }
        }
    }
}
