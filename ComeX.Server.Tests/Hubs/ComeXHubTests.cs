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
        const string TOKEN = "XVcrlCT3F0qvIUMa8fTGH8n94ZSIXqWlTAlrFO8taIY=";
        const string USERNAME = "IntegrationTestUsernameForTokenTests";

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
            await connection.InvokeAsync("SendLoginMessage", new LoginMessage(TOKEN));

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
                rsp.RoomsList.Should().ContainSingle(room => room.RoomId == Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040") && !room.IsArchived);
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SentRoomRequest", new RoomRequest(TOKEN));

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

            var chatMessage = new ChatMessage(TOKEN, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), null, "automated test message");

            connection.On<MessageResponse>("Message_created", (rsp) =>
            {
                rsp.Should().NotBeNull();
                rsp.ParentId.Should().Be(chatMessage.ParentId);
                rsp.RoomId.Should().Be(chatMessage.RoomId);
                rsp.Content.Should().Be(chatMessage.Content);
                rsp.EmojiList.Should().BeEmpty();
                rsp.SendTime.Should().NotBeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
                rsp.Username.Should().Be(USERNAME);
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SendChatMessage", chatMessage);

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

            var msg = new LoadMessageRequest(TOKEN, Guid.Parse("d9feaa19-4d5f-42a2-90f7-e91ef2d808a5"));

            connection.On<LoadMessageResponse>("Load_message", (rsp) =>
            {
                rsp.Message.Id.Should().Be(msg.Id);
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("LoadSpecificMessage", msg);

            VerifyMock(mock);
        }

        [TestMethod()]
        public async Task LoadSpecificSurveyTest()
        {
            await SendLoginMessageTest();
            var mock = new Mock<ITest>();
            HubConnection connection = null;

            mock.Setup(x => x.TestMethod()).Verifiable();
            connection = GetNewConnection();

            var msg = new LoadMessageRequest(TOKEN, Guid.Parse("dacd1ea3-e135-40ef-b13a-7e637f866a88"));

            connection.On<SurveyVoteResponse>("Load_survey", (rsp) =>
            {
                rsp.Survey.Id.Should().Be(msg.Id);
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("LoadSpecificSurvey", msg);

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
                rsp.MessageList.Should().Contain(rsp => rsp.RoomId == Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"));
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("LoadChatHistory", new LoadChatRequest(TOKEN, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), DateTime.Now));

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

            connection.On<LoadSurveyVoteResponse>("Send_survey_history", (rsp) =>
            {
                rsp.SurveyVoteList.Should().Contain(rsp => rsp.Survey.RoomId == Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"));
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("LoadSurveyHistory", new LoadSurveyRequest(TOKEN, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), DateTime.Now));
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
                rsp.SurveyVoted.RoomId.Should().Be(Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"));
                rsp.MessageList.Should().Contain(rsp => rsp.RoomId == Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"));
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("LoadAllHistory", new LoadChatRequest(TOKEN, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), DateTime.Now));
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

            List<string> answerList = new List<string>();
            answerList.Add("Test answer 1");
            answerList.Add("Test answer 2");
            SurveyMessage msg = new SurveyMessage(TOKEN, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), "Test question", answerList);

            connection.On<SurveyVoteResponse>("Survey_created", (rsp) =>
            {
                rsp.Survey.RoomId.Should().Be(msg.RoomId);
                rsp.Survey.Question.Should().Be(msg.Question);
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SendChatSurvey", msg);
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

            List<Guid> votes = new List<Guid>();
            votes.Add(Guid.Parse("db6ec4e2-f550-47f5-a458-2a1554bd755e"));
            votes.Add(Guid.Parse("32765081-eae2-49f9-83c0-18d77e86229f"));
            SurveyVoteMessage msg = new SurveyVoteMessage(TOKEN, Guid.Parse("bb90b28c-912c-429e-9ea3-6aac41b17356"), votes);

            connection.On<SurveyResponse>("Survey_updated", (rsp) =>
            {
                rsp.Id.Should().Be(msg.SurveyId);
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SendChatSurveyVote", msg);
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
                rsp.MessageList.Should().Contain(rsp => rsp.RoomId == Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"));
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("SearchMessage", new SearchMessageRequest(TOKEN, Guid.Parse("6e36c4f8-e2b3-4638-afd0-fafc4340b040"), "test"));
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

            ReactionMessage msg = new ReactionMessage(TOKEN, Guid.Parse("eb20efc8-afd5-40fe-b7c8-7a8d7ab1bde4"), "1");

            connection.On<LoadMessageResponse>("Message_updated", (rsp) =>
            {
                rsp.Message.Id.Should().Be(msg.MessageId);
                mock.Object.TestMethod();
            });

            await connection.StartAsync();
            await connection.InvokeAsync("AddReaction", msg);
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