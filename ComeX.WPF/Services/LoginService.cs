using ComeX.Lib.Common.UserDatabaseAPI;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.Services {
    public class LoginService {
        private readonly HubConnection _connection;

        public event Action<TokenDataModel> LoginTokenReceived;

        public LoginService(HubConnection connection) {
            _connection = connection;
            //_connection.On<LoginDataModel>("ReceiveTokenLogin", (message) => LoginTokenReceived?.Invoke(TokenDataModel));
        }

        public async Task Connect() {
            await _connection.StartAsync();
        }

        public async Task Login(LoginDataModel login) {
        //    await _connection.SendAsync("Login", login);
        }
    }
}
