using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace ComeX.WPF.Services {
    public class LoginService {
        private static readonly HttpClient _httpClient = new HttpClient();
        private UriBuilder _uriBuilderUser;
        private UriBuilder _uriBuilderServer;

        public event Action<TokenDataModel> LoginTokenReceived;

        public LoginService() {
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();

            _uriBuilderUser = new UriBuilder("https://localhost:44327/api/user/");
            _uriBuilderServer = new UriBuilder("https://localhost:44327/api/server/");
        }

        public async Task<LoginDataModel> Login(string login, string password) {
            var query = HttpUtility.ParseQueryString(_uriBuilderUser.Query);
            query["username"] = login;
            query["password"] = password;
            _uriBuilderUser.Query = query.ToString();
            string url = _uriBuilderUser.ToString();

            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ArgumentException();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LoginDataModel>(content);
        }

        public async Task<LoginDataModel> Register(string login, string password) {
            var query = HttpUtility.ParseQueryString(_uriBuilderUser.Query);
            query["username"] = login;
            query["password"] = password;
            _uriBuilderUser.Query = query.ToString();
            string url = _uriBuilderUser.ToString();

            var response = await _httpClient.PostAsync(url, null);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ArgumentException();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LoginDataModel>(content);
        }

        public async Task<bool> ChangePassword(string login, string oldPassword, string newPassword) {
            var query = HttpUtility.ParseQueryString(_uriBuilderUser.Query);
            query["username"] = login;
            query["password"] = oldPassword;
            query["newPassword"] = newPassword;
            _uriBuilderUser.Query = query.ToString();
            string url = _uriBuilderUser.ToString();

            var response = await _httpClient.PutAsync(url, null);
            if (response.StatusCode == System.Net.HttpStatusCode.OK) return true;
            else return false;
        }

        public async Task<IEnumerable<ServerDataModel>> GetServers(string login) {
            _httpClient.DefaultRequestHeaders.Add("username", login);
            string url = _uriBuilderServer.ToString();

            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ArgumentException();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ServerDataModel>>(content);
        }
    }
}
