using ComeX.Lib.Common.UserDatabaseAPI;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace ComeX.UserDatabaseAPI.Tests
{
    [TestClass()]
    public class IntegrationTests
    {
        private TestServer _server;
        private HttpClient _httpClient;
        private const string TOKEN_HASH = "5B60C4910BBEF1641035AD057C8F292503DAF706358C4A858CBC055932C7CAEF42735E16F5AE5B8A49D06812F84628A41393B5F6F80A1793A3F873CAE8398B97";
        private const string USERNAME = "IntegrationTestUsernameForTokenTests";
        private const string USER_ID = "34dd3d37-2506-4acf-925e-452d809bcfa9";
        private const string DATE ="21.01.2580 20:14:41";

        [TestInitialize]
        public void TestInit()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build())
                .UseStartup<Startup>();
            _server = new TestServer(webHostBuilder);
            _httpClient = _server.CreateClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _server.Dispose();
        }

        private string GetUrlToUserEndpoints(string username, string password, string newPassword = null)
        {
            var testUri = new UriBuilder(_httpClient.BaseAddress.ToString() + "api/User");
            var query = HttpUtility.ParseQueryString(testUri.Query);
            query["username"] = username;
            query["password"] = password;
            if (newPassword != null)
                query["newPassword"] = newPassword;
            testUri.Query = query.ToString();
            return testUri.ToString();
        }

        private string GetUrlToAuthEndpoint()
        {
            var testUri = new UriBuilder(_httpClient.BaseAddress.ToString() + "api/Auth");
            var query = HttpUtility.ParseQueryString(testUri.Query);
            query["tokenHash"] = TOKEN_HASH;
            testUri.Query = query.ToString();
            return testUri.ToString();
        }

        private string GetUrlToServerEndpoints(string endpoint, string name, string url, string username)
        {
            var testUri = new UriBuilder(_httpClient.BaseAddress.ToString() + endpoint);
            var query = HttpUtility.ParseQueryString(testUri.Query);
            if (username != null)
                query["username"] = username;
            if (name != null)
                query["name"] = name;
            if (url != null)
                query["url"] = url;
            testUri.Query = query.ToString();
            return testUri.ToString();
        }

        [TestMethod()]
        public async Task CreateAndDeleteUserTest()
        {
            var url = GetUrlToUserEndpoints("IntegrationTestUsername", "IntegrationTestPassword");

            var createResponse = await _httpClient.PostAsync(url, null);
            var deleteResponse = await _httpClient.DeleteAsync(url);

            Assert.AreEqual(HttpStatusCode.OK, createResponse.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [TestMethod()]
        public async Task LoginTest()
        {
            var url = GetUrlToUserEndpoints("IntegrationTestUsername", "IntegrationTestPassword");

            var createResponse = await _httpClient.PostAsync(url, null);
            var loginResponse = await _httpClient.GetAsync(url);
            var content = await loginResponse.Content.ReadAsStringAsync();
            var deleteResponse = await _httpClient.DeleteAsync(url);

            var result = JsonSerializer.Deserialize<LoginDataModel>(content);
            var username = "IntegrationTestUsername";

            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
            Assert.AreEqual(username, result.Username);
        }

        [TestMethod()]
        public async Task UpdateTest()
        {
            var url = GetUrlToUserEndpoints("IntegrationTestUsername", "IntegrationTestPassword");
            var updateUrl = GetUrlToUserEndpoints("IntegrationTestUsername", "IntegrationTestPassword", "IntegrationTestPassword2");
            var deleteUrl = GetUrlToUserEndpoints("IntegrationTestUsername", "IntegrationTestPassword2");

            var createResponse = await _httpClient.PostAsync(url, null);
            var updateResponse = await _httpClient.PutAsync(updateUrl, null);
            var deleteResponse = await _httpClient.DeleteAsync(deleteUrl);

            Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
        }

        [TestMethod()]
        public async Task GetTokenInfoTest()
        {
            var url = GetUrlToAuthEndpoint();

            var tokenDataModel = new TokenDataModel(USER_ID, USERNAME, DATE);
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TokenDataModel>(content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(tokenDataModel, result);
        }

        [TestMethod()]
        public async Task CreateAndDeleteServerTest()
        {
            var createUrl = GetUrlToServerEndpoints("api/Server", "IntegrationServerTest", "integration.com", null);
            var deleteUrl = GetUrlToServerEndpoints("api/Server", null, "integration.com", null);

            var createResponse = await _httpClient.PostAsync(createUrl, null);
            var deleteResponse = await _httpClient.DeleteAsync(deleteUrl);

            Assert.AreEqual(HttpStatusCode.OK, createResponse.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [TestMethod()]
        public async Task GetServersAndAddUserToServerTest()
        {
            var createFirstUrl = GetUrlToServerEndpoints("api/Server", "IntegrationServerTest", "integration.com", null);
            var createSecondUrl = GetUrlToServerEndpoints("api/Server", "IntegrationServerTest2", "integration2.com", null);
            var deleteFirstUrl = GetUrlToServerEndpoints("api/Server", null, "integration.com", null);
            var deleteSecondUrl = GetUrlToServerEndpoints("api/Server", null, "integration2.com", null);
            var createUserUrl = GetUrlToUserEndpoints("IntegrationTestUsername", "IntegrationTestPassword");
            var addToFirstServerUrl = GetUrlToServerEndpoints("api/Server/user", null, "integration.com", "IntegrationTestUsername");
            var addToSecondServerUrl = GetUrlToServerEndpoints("api/Server/user", null, "integration2.com", "IntegrationTestUsername");
            var deleteUserUrl = GetUrlToUserEndpoints("IntegrationTestUsername", "IntegrationTestPassword");
            var getServersUrl = GetUrlToServerEndpoints("api/Server", null, null, "IntegrationTestUsername");

            var createFirstResponse = await _httpClient.PostAsync(createFirstUrl, null);
            var createSecondResponse = await _httpClient.PostAsync(createSecondUrl, null);
            var createUserResponse = await _httpClient.PostAsync(createUserUrl, null);
            var addToFirstResponse = await _httpClient.PostAsync(addToFirstServerUrl, null);
            var addToSecondResponse = await _httpClient.PostAsync(addToSecondServerUrl, null);
            var getServersResponse = await _httpClient.GetAsync(getServersUrl);
            var content = await getServersResponse.Content.ReadAsStringAsync();
            var deleteUserResponse = await _httpClient.DeleteAsync(deleteUserUrl);
            var deleteFirstResponse = await _httpClient.DeleteAsync(deleteFirstUrl);
            var deleteSecondResponse = await _httpClient.DeleteAsync(deleteSecondUrl);

            var result = JsonSerializer.Deserialize<IEnumerable<ServerDataModel>>(content);
            IEnumerable<ServerDataModel> servers = new List<ServerDataModel>()
            {
                new ServerDataModel("IntegrationServerTest", "integration.com"),
                new ServerDataModel("IntegrationServerTest2", "integration2.com")
            };

            Assert.AreEqual(HttpStatusCode.OK, getServersResponse.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, addToFirstResponse.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, addToSecondResponse.StatusCode);
            servers.Should().Contain(x => x.Name == result.ElementAt(0).Name && x.Url == result.ElementAt(0).Url);
            servers.Should().Contain(x => x.Name == result.ElementAt(1).Name && x.Url == result.ElementAt(1).Url);
        }
    }
}
