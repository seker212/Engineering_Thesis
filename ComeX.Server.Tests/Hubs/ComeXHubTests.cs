using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComeX.Server.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ComeX.Lib.Auth;
using Microsoft.AspNetCore.SignalR;
using System.Dynamic;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR.Client;
using ComeX.Lib.Common.ServerCommunicationModels;
using System.Diagnostics;

namespace ComeX.Server.Hubs.Tests
{
    [TestClass()]
    public class ComeXHubTests
    {
        [TestMethod()]
        public void ComeXHubTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public async Task SendLoginMessageTest()
        {
            TestServer server = null;
            string m = null;
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>();
            server = new TestServer(webHostBuilder);
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost/ComeXLogin", o => o.HttpMessageHandlerFactory = _ => server.CreateHandler())
                .Build();

            connection.On<string>("SendLoginMessage", msg =>
            {
                m = msg;
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SendLoginMessage", new LoginMessage("2lNwrCIvaNcXrwgnIIXwXsCC0bM3lknARHqFggUu1fA="));
            Debug.WriteLine(m);
        }

        [TestMethod()]
        public void SentRoomRequestTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SendChatMessageTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void LoadSpecificMessageTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void LoadChatHistoryTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void LoadSurveyHistoryTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void LoadAllHistoryTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SendChatSurveyTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SendChatSurveyVoteTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SearchMessageTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void AddReactionTest()
        {
            throw new NotImplementedException();
        }
    }
}