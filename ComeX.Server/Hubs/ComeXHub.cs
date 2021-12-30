﻿using ComeX.Lib.Auth;
using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using ComeX.Server.DAL;
using ComeX.Server.DatabaseModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.Hubs
{
    public class ComeXHub : Hub
    {
        ILoginManager _loginManager;
        IConnectionCache _connectionCache;
        UserRepository usrRepo;
        RoomRepository roomRepo;
        MessageRepository msgRepo;
        ReactionRepository reactRepo;
        SurveyRepository srvRepo;
        AnswerRepository ansRepo;
        VoteRepository votRepo;

        public ComeXHub(ILoginManager loginManager, IConnectionCache connectionCache)
        {
            _loginManager = loginManager;
            _connectionCache = connectionCache;
            usrRepo = new UserRepository("Server = 127.0.0.1; Port = 5432; Database = postgres; User Id = postgres; Password = mysecretpassword;");
            roomRepo = new RoomRepository("Server = 127.0.0.1; Port = 5432; Database = postgres; User Id = postgres; Password = mysecretpassword;");
            msgRepo = new MessageRepository("Server = 127.0.0.1; Port = 5432; Database = postgres; User Id = postgres; Password = mysecretpassword;");
            reactRepo = new ReactionRepository("Server = 127.0.0.1; Port = 5432; Database = postgres; User Id = postgres; Password = mysecretpassword;");
            srvRepo = new SurveyRepository("Server = 127.0.0.1; Port = 5432; Database = postgres; User Id = postgres; Password = mysecretpassword;");
            ansRepo = new AnswerRepository("Server = 127.0.0.1; Port = 5432; Database = postgres; User Id = postgres; Password = mysecretpassword;");
            votRepo = new VoteRepository("Server = 127.0.0.1; Port = 5432; Database = postgres; User Id = postgres; Password = mysecretpassword;");
        }

        public async Task SendLoginMessage(LoginMessage msg)
        {
            //Klient wysyła mi tu token i ja sobie pobieram dane o nim
            _loginManager.Login(Context.ConnectionId, msg.Token);
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            //TODO sprawdzanie whitelisty i blacklisty
            try
            {
                User loginUser = usrRepo.GetUser(Guid.Parse(_connectionCache[msg.Token].UserId));
                await Clients.Caller.SendAsync("Logged_in");
            } catch(Exception e)
            {
                User loginUser = usrRepo.InsertUser(new User(usrId, usrName));
                await Clients.Caller.SendAsync("First_login");
            }
        }

        //zapytanie o pokoje
        public async Task SentRoomRequest(RoomRequest rqst)
        {
            try
            {
                List<RoomResponse> roomList = new List<RoomResponse>();
                List<Room> tmprooms = roomRepo.GetRooms().ToList();

                foreach(Room tmproom in tmprooms)
                {
                    RoomResponse room = new RoomResponse(tmproom.Id, tmproom.Name, tmproom.IsArchived);
                    roomList.Add(room);
                }

                RoomsListResponse rsp = new RoomsListResponse(roomList);

                await Clients.Caller.SendAsync("Sending_rooms", rsp);

            } catch (Exception e)
            {
                await Clients.Caller.SendAsync("Server_room_error");
            }
        }

        // otrzymano message
        public async Task SendChatMessage(ChatMessage msg) // token, room id, parent id, content
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;

            try
            {
                Message insertMsg = new Message(Guid.NewGuid(), usrId, false, msg.RoomId, DateTime.Now.ToString(), msg.ParentId, msg.Content);
                Message createdMsg = msgRepo.InsertMessage(insertMsg);

                IEnumerable<Reaction> reactions = reactRepo.GetReactions(createdMsg.Id);
                Dictionary<string, int> emojiList = new Dictionary<string, int>();
                foreach (Reaction r in reactions)
                {
                    try
                    {
                        emojiList.Add(r.Emoji, 1);
                    } catch (ArgumentException)
                    {
                        emojiList[r.Emoji] += 1;
                    }
                    
                }

                MessageResponse rsp = new MessageResponse(createdMsg.Id, usrName, createdMsg.SendTime, createdMsg.RoomId, createdMsg.ParentId, createdMsg.Content, emojiList);
                
                await Clients.Caller.SendAsync("Message_created", rsp);

            } catch (Exception e)
            {
                await Clients.Caller.SendAsync("Server_message_error");
            }
            
        }

        // otrzymano żądanie wczytania wiadomości
        public async Task LoadChatHistory()
        {
            await Clients.Caller.SendAsync("ReceiveLoadChatHistory");
        }

        // otrzymano żądanie wczytania ankiet
        public async Task LoadSurveyHistory()
        {
            await Clients.Caller.SendAsync("ReceiveLoadChatHistory");
        }

        // otrzymano ankietę (zawiera dopuszczalne odpowiedzi)
        public async Task SendChatSurvey(SurveyMessage msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;

            try
            {
                Survey insertSrv = new Survey(Guid.NewGuid(), usrId, msg.RoomId, DateTime.Now.ToString(), msg.Question, msg.IsMultipleChoice);
                Survey createdSrv = srvRepo.Insert(insertSrv);

                foreach (string s in msg.AnswerList)
                {
                    try
                    {
                        Answer insertAns = new Answer(Guid.NewGuid(), s, createdSrv.Id);
                        Answer createdAns = ansRepo.Insert(insertAns);

                    } catch (Exception e)
                    {
                        await Clients.Caller.SendAsync("Server_survey_answer_error");
                    }
                    
                }

                Dictionary<SurveyAnswerResponse, int> ansList = new Dictionary<SurveyAnswerResponse, int>();
                IEnumerable<Answer> answers = ansRepo.GetAnswers(createdSrv.Id);

                foreach (Answer ans in answers)
                {
                    SurveyAnswerResponse rsp = new SurveyAnswerResponse(ans.Id, ans.Content);

                    IEnumerable<Vote> votes = votRepo.GetVotes(ans.Id);
                    int amount = votes.Count();

                    ansList.Add(rsp, amount);
                }

                SurveyResponse response = new SurveyResponse(usrName, createdSrv.SendTime, createdSrv.RoomId, createdSrv.Question, createdSrv.IsMultipleChoice, ansList);

                await Clients.Caller.SendAsync("Survey_created", response);

            } catch (Exception e)
            {
                await Clients.Caller.SendAsync("Server_survey_error");
            }

        }

        // otrzymano głos do ankiety
        public async Task SendChatSurveyVote()
        {
            await Clients.Caller.SendAsync("ReceiveChatSurveyVote");
        }
    }
}
