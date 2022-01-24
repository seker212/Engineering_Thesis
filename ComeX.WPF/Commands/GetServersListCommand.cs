using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Models;
using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class GetServersListCommand : ICommand {
        private readonly ChatViewModel _viewModel;
        private readonly LoginService _loginService;

        public GetServersListCommand(ChatViewModel viewModel, LoginService loginService) {
            _viewModel = viewModel;
            _loginService = loginService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                IEnumerable<ServerDataModel> serverDataModels = await _loginService.GetServers(_viewModel.LoginDM.Username);

                foreach (var serverDM in serverDataModels) {
                    if (!_viewModel.ServerDMs.Any(o => o.Url == serverDM.Url)) {
                        _viewModel.ServerDMs.Add(serverDM);
                        ServerViewModel newServer = new ServerViewModel(serverDM.Url, serverDM.Name, _viewModel);
                        _viewModel.Servers.Add(newServer);
                    }
                }

            } catch (Exception e) {
                _viewModel.ErrorMessage = "Server list refresh failed";
            }
        }
    }
}
