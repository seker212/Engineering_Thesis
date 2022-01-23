using ComeX.Lib.Common.UserDatabaseAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace ComeX.WPF.Services {
    public class LoginService {
        private static readonly HttpClient _httpClient = new HttpClient();
        //private const string UserApiBaseUri = "https://localhost:44327/api";
        private const string UserApiBaseUri = "https://comx.molly.ovh:40443/api";
        private UriBuilder _uriBuilderUser;
        private UriBuilder _uriBuilderServer;
        private UriBuilder _uriBuilderUserServer;

        public event Action<TokenDataModel> LoginTokenReceived;

        public LoginService() {
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();

            _uriBuilderUser = new UriBuilder(UserApiBaseUri + "/user/");
            _uriBuilderServer = new UriBuilder(UserApiBaseUri + "/server/");
            _uriBuilderUserServer = new UriBuilder(UserApiBaseUri + "/server/user");
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

        public async Task<bool> Register(string login, string password) {
            var query = HttpUtility.ParseQueryString(_uriBuilderUser.Query);
            query["username"] = login;
            query["password"] = password;
            _uriBuilderUser.Query = query.ToString();
            string url = _uriBuilderUser.ToString();

            var response = await _httpClient.PostAsync(url, null);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ArgumentException();
            return true;
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

        public async Task<bool> DeleteAccount(string login, string password) {
            var query = HttpUtility.ParseQueryString(_uriBuilderUser.Query);
            query["username"] = login;
            query["password"] = password;
            _uriBuilderUser.Query = query.ToString();
            string url = _uriBuilderUser.ToString();

            var response = await _httpClient.DeleteAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) return false;
            else return true;
        }

        public async Task<IEnumerable<ServerDataModel>> GetServers(string login) {
            var query = HttpUtility.ParseQueryString(_uriBuilderServer.Query);
            query["username"] = login;
            _uriBuilderServer.Query = query.ToString();
            string url = _uriBuilderServer.ToString();

            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ArgumentException();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ServerDataModel>>(content);
        }

        public async Task<bool> JoinServer(string login, string serverUrl) {
            var query = HttpUtility.ParseQueryString(_uriBuilderUserServer.Query);
            query["username"] = login;
            query["url"] = serverUrl;
            _uriBuilderUserServer.Query = query.ToString();
            string url = _uriBuilderUserServer.ToString();

            var response = await _httpClient.PostAsync(url, null);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ArgumentException();
            return true;
        }
    }
}
