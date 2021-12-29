using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.Models {
    public class ServerClientModel {
        public string Name;
        public string Url;
        public HubConnection Connection;
        public ChatService Service;
        public List<RoomClientModel> RoomList;

        public ServerClientModel(string url) {
            // http://localhost:5000/ComeXLogin
            Url = url;
            Connection = new HubConnectionBuilder()
                .WithUrl(url+"/ComeXLogin")
                .Build();

            Service = new ChatService(Connection);

            try {
                Service.Connect().ContinueWith(task => {
                    if (task.Exception != null) {
                        //ErrorMessage = "Unable to connect to chat server";
                    }
                });
            } catch (Exception e) { }
        }

        public void LoginToServer(LoginDataModel loginDM) {
            Connection = new HubConnectionBuilder()
                .WithUrl(Url + "/ComeXLogin")
                .Build();

            Service = new ChatService(Connection);

            try {
                Service.Connect().ContinueWith(task => {
                    if (task.Exception != null) {
                        //ErrorMessage = "Unable to connect to chat server";
                    }
                });

                Service.SendLoginMessage(new LoginMessage(loginDM.Token)).ContinueWith(task => {
                    if (task.Exception != null) {
                        //ErrorMessage = "Unable to send login message to chat server";
                    }
                });
            } catch (Exception e) {

            }
        }
    }
}
