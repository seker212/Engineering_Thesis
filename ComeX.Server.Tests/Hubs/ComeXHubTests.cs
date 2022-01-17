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
using ComeX.Lib.Common.ServerResponseModels;

namespace ComeX.Server.Hubs.Tests
{
    public interface ITest
    {
        void TestMethod();
    }

    [TestClass()]
    public class ComeXHubTests
    {
        TestServer server;
        const string token = "2lNwrCIvaNcXrwgnIIXwXsCC0bM3lknARHqFggUu1fA=";

        [TestInitialize]
        public void TestInit()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseStartup<Startup>();
            server = new TestServer(webHostBuilder);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            server.Dispose();
        }

        [TestMethod()]
        public async Task SendLoginMessageTest()
        {
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On("Logged_in", () =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SendLoginMessage", new LoginMessage(token));

            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task SentRoomRequestTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On<RoomsListResponse>("Sending_rooms", (rsp) =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SentRoomRequest", new RoomRequest(token));

            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task SendChatMessageTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On<MessageResponse>("Message_created", (rsp) =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SendChatMessage", new ChatMessage(token, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), null, "automated test message"));

            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task LoadSpecificMessageTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On<LoadMessageResponse>("Load_message", (rsp) =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("LoadSpecificMessage", new LoadMessageRequest(token, Guid.Parse("d9feaa19-4d5f-42a2-90f7-e91ef2d808a5")));

            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task LoadChatHistoryTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On<LoadChatResponse>("Send_chat_history", (rsp) =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("LoadChatHistory", new LoadChatRequest(token, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), DateTime.Now));

            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task LoadSurveyHistoryTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On<LoadSurveyResponse>("Send_survey_history", (rsp) =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("LoadSurveyHistory", new LoadSurveyRequest(token, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), DateTime.Now));
            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task LoadAllHistoryTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On<LoadAllResponse>("Send_all_history", (rsp) =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("LoadAllHistory", new LoadChatRequest(token, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), DateTime.Now));
            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task SendChatSurveyTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On<SurveyResponse>("Survey_created", (rsp) =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            List<string> answerList = new List<string>();
            answerList.Add("Test answer 1");
            answerList.Add("Test answer 2");
            await connection.InvokeAsync("SendChatSurvey", new SurveyMessage(token, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), "Test question", answerList));
            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task SendChatSurveyVoteTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On<SurveyResponse>("Survey_updated", (rsp) =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            List<Guid> votes = new List<Guid>();
            votes.Add(Guid.Parse("db6ec4e2-f550-47f5-a458-2a1554bd755e"));
            votes.Add(Guid.Parse("32765081-eae2-49f9-83c0-18d77e86229f"));
            await connection.InvokeAsync("SendChatSurveyVote", new SurveyVoteMessage(token, Guid.Parse("bb90b28c-912c-429e-9ea3-6aac41b17356"), votes));
            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task SearchMessageTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On<LoadChatResponse>("Send_search", (rsp) =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SearchMessage", new SearchMessageRequest(token, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), "test"));
            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task AddReactionTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            connection.On<LoadMessageResponse>("Message_updated", (rsp) =>
            {
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("AddReaction", new ReactionMessage(token, Guid.Parse("eb20efc8-afd5-40fe-b7c8-7a8d7ab1bde4"), "1"));
            VerifyMock(mock);
        }

        HubConnection GetNewConnection()
        {
            return new HubConnectionBuilder()
                .WithUrl("http://localhost/ComeXLogin", o => o.HttpMessageHandlerFactory = _ => server.CreateHandler())
                .Build();
        }

        private static void VerifyMock(Mock<ITest> mock)
        {
            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    mock.Verify();
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(5);
                }
            }
            mock.Verify();
        }
    }
}