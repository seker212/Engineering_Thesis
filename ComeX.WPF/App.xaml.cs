using ComeX.WPF.ViewModels;
using ComeX.WPF.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ComeX.WPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/ComeXChat")
                .Build();

            ChatViewModel chatViewModel = ChatViewModel.CreatedConnectedModel(new SignalRChatService(connection));

            LoginViewModel loginViewModel = LoginViewModel.CreatedConnectedModel(new LoginService(connection));

            MainWindow window = new MainWindow() {
                DataContext = new MainViewModel(loginViewModel)
                //DataContext = new MainViewModel(chatViewModel)
            };
            window.Show();
        }
    }
}
