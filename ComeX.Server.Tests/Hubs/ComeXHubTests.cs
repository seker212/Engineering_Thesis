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
using FluentAssertions;
using System.Threading;

namespace ComeX.Server.Hubs.Tests
{
    public interface ITest
    {
        void TestMethod();
    }

    [TestClass()]
    public class ComeXHubTests
    {
        static TestServer server;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>();
            server = new TestServer(webHostBuilder);
        }

        [TestMethod()]
        public void ComeXHubTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public async Task SendLoginMessageTest()
        {
            bool invoked = false;
            HubConnection connection = null;
            connection = GetNewConnection();

            connection.On("Logged_in", () => {
                invoked = true;
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SendLoginMessage", new LoginMessage("2lNwrCIvaNcXrwgnIIXwXsCC0bM3lknARHqFggUu1fA="));
            
            for (int i = 0; i < 1000; i++)
            {
                if (invoked)
                    break;
                Thread.Sleep(5);
            }
            invoked.Should().BeTrue();
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

        HubConnection GetNewConnection()
        {
            return new HubConnectionBuilder()
                .WithUrl("http://localhost/ComeXLogin", o => o.HttpMessageHandlerFactory = _ => server.CreateHandler())
                .Build();
        }
    }
}