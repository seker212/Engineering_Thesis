using ComeX.Lib.Common.UserDatabaseAPI;
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
        private UriBuilder _uriBuilder;

        public event Action<TokenDataModel> LoginTokenReceived;

        public LoginService(HubConnection connection) {
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();

            _uriBuilder = new UriBuilder("https://localhost:44327/api/user/");
        }

        public async Task<LoginDataModel> Login(string login, string password) {
            var query = HttpUtility.ParseQueryString(_uriBuilder.Query);
            query["username"] = login;
            query["password"] = password;
            _uriBuilder.Query = query.ToString();
            string url = _uriBuilder.ToString();

            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ArgumentException();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LoginDataModel>(content);
        }

        public async Task<LoginDataModel> Register(string login, string password) {
            var query = HttpUtility.ParseQueryString(_uriBuilder.Query);
            query["username"] = login;
            query["password"] = password;
            _uriBuilder.Query = query.ToString();
            string url = _uriBuilder.ToString();

            var response = await _httpClient.PostAsync(url, null);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ArgumentException();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LoginDataModel>(content);
        }
    }
}
