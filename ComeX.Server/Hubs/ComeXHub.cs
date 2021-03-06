using ComeX.Lib.Auth;
using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
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
        Allowed_userRepository allowRepo;

        public ComeXHub(ILoginManager loginManager, IConnectionCache connectionCache)
        {
            _loginManager = loginManager;
            _connectionCache = connectionCache;
            usrRepo = new UserRepository(Startup.SELF_DATABASE_URL);
            roomRepo = new RoomRepository(Startup.SELF_DATABASE_URL);
            msgRepo = new MessageRepository(Startup.SELF_DATABASE_URL);
            reactRepo = new ReactionRepository(Startup.SELF_DATABASE_URL);
            srvRepo = new SurveyRepository(Startup.SELF_DATABASE_URL);
            ansRepo = new AnswerRepository(Startup.SELF_DATABASE_URL);
            votRepo = new VoteRepository(Startup.SELF_DATABASE_URL);
            allowRepo = new Allowed_userRepository(Startup.SELF_DATABASE_URL);
        }

        //sprawdzanie czy uzytkownik jest na whitelist
        private bool CheckWhitelist(Guid usrId, string username)
        {
            Allowed_user user = allowRepo.GetAllowed_user(usrId);
            if (user == null && usrRepo.UpdateToNormalUser(usrId, username))
            {
                allowRepo.Insert(new Allowed_user(usrId));
                return true;
            }
            else if (user != null)
                return true;
            return false;
        }

        private bool CheckRoomWriteStatus(Guid roomId)
        {
            Room check = roomRepo.GetRoom(roomId);
            if (check.IsArchived)
            {
                return false;
            } else
            {
                return true;
            }
        }

        // logowanie do serwera
        public async Task SendLoginMessage(LoginMessage msg)
        {
            //Klient wysyła mi tu token i ja sobie pobieram dane o nim
            _loginManager.Login(Context.ConnectionId, msg.Token);
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;

            if (CheckWhitelist(usrId, usrName))
            {
                //wpisanie do users
                try
                {
                    //gdy uzytkownik loguje sie po raz pierwszy
                    User loginUser = usrRepo.GetUser(Guid.Parse(_connectionCache[msg.Token].UserId));
                    await Clients.Caller.SendAsync("Logged_in");
                }
                catch (Exception e)
                {
                    //gdy uzytkownik loguje sie po raz kolejny
                    await Clients.Caller.SendAsync("Repeat_login");

                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }
        }

        //zapytanie o pokoje
        public async Task SentRoomRequest(RoomRequest rqst)
        {
            Guid usrId = Guid.Parse(_connectionCache[rqst.Token].UserId);
            string usrName = _connectionCache[rqst.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                //wczytanie i wyslanie listy pokojow
                try
                {
                    List<RoomResponse> roomList = new List<RoomResponse>();
                    List<Room> tmprooms = roomRepo.GetRooms().ToList();

                    foreach (Room tmproom in tmprooms)
                    {
                        RoomResponse room = new RoomResponse(tmproom.Id, tmproom.Name, tmproom.IsArchived);
                        roomList.Add(room);
                    }

                    RoomsListResponse rsp = new RoomsListResponse(roomList);

                    await Clients.Caller.SendAsync("Sending_rooms", rsp);

                }
                catch (Exception e)
                {
                    await Clients.Caller.SendAsync("Server_room_error");
                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }
        }

        // otrzymano message
        public async Task SendChatMessage(ChatMessage msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                //sprawdzenie czy do pokoju mozna zapisywac
                if (CheckRoomWriteStatus(msg.RoomId))
                {
                    //dodanie wiadomosci do bazy danych
                    try
                    {
                        Message insertMsg = new Message(Guid.NewGuid(), usrId, msg.RoomId, DateTime.Now, msg.ParentId, msg.Content);
                        Message createdMsg = msgRepo.InsertMessage(insertMsg);

                        await Clients.Caller.SendAsync("ACK");

                        //wczytanie wiadomosci i reakcji na nia i wyslanie do kazdego polaczonego klienta
                        IEnumerable<Reaction> reactions = reactRepo.GetReactions(createdMsg.Id);
                        Dictionary<string, int> emojiList = new Dictionary<string, int>();
                        foreach (Reaction r in reactions)
                        {
                            try
                            {
                                emojiList.Add(r.Emoji, 1);
                            }
                            catch (ArgumentException)
                            {
                                emojiList[r.Emoji] += 1;
                            }

                        }

                        MessageResponse rsp = new MessageResponse(createdMsg.Id, usrName, createdMsg.SendTime, createdMsg.RoomId, createdMsg.ParentId, createdMsg.Content, emojiList);

                        await Clients.All.SendAsync("Message_created", rsp);

                    }
                    catch (Exception e)
                    {
                        await Clients.Caller.SendAsync("Server_message_error");
                    }
                } else
                {
                    await Clients.Caller.SendAsync("Room_archived");
                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }   
        }

        // otrzymano zadanie wczytania konkretnej wiadomosci
        public async Task LoadSpecificMessage(LoadMessageRequest msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                try
                {
                    Message message = msgRepo.GetMessage(msg.Id);
                    User creator = usrRepo.GetUser(message.AuthorId);

                    IEnumerable<Reaction> reactions = reactRepo.GetReactions(message.Id);
                    Dictionary<string, int> emojiList = new Dictionary<string, int>();
                    foreach (Reaction r in reactions)
                    {
                        try
                        {
                            emojiList.Add(r.Emoji, 1);
                        }
                        catch (ArgumentException)
                        {
                            emojiList[r.Emoji] += 1;
                        }

                    }

                    MessageResponse rsp = new MessageResponse(message.Id, creator.Username, message.SendTime, message.RoomId, message.ParentId, message.Content, emojiList);
                    LoadMessageResponse response = new LoadMessageResponse(rsp);

                    await Clients.Caller.SendAsync("Load_message", response);
                }
                catch (Exception e)
                {
                    await Clients.Caller.SendAsync("Load_message_error");
                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }
        }

        // otrzymano zadanie wczytania konkretnej ankiety
        public async Task LoadSpecificSurvey(LoadMessageRequest msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                try
                {
                    Survey searchSrv = srvRepo.Get(msg.Id);
                    bool voted = false;

                    User usr = usrRepo.GetUser(searchSrv.AuthorId);

                    IEnumerable<Answer> answers = ansRepo.GetAnswers(searchSrv.Id);
                    List<SurveyAnswerResponse> ansList = new List<SurveyAnswerResponse>();

                    foreach (Answer ans in answers)
                    {

                        IEnumerable<Vote> votes = votRepo.GetVotes(ans.Id);
                        int amount = votes.Count();

                        SurveyAnswerResponse rsp = new SurveyAnswerResponse(ans.Id, ans.Content, amount);

                        ansList.Add(rsp);

                        Vote checkUser = votRepo.GetVote(ans.Id, usrId);
                        if (checkUser != null)
                        {
                            voted = true;
                        }
                    }

                    SurveyResponse response = new SurveyResponse(searchSrv.Id, usr.Username, searchSrv.SendTime, searchSrv.RoomId, searchSrv.Question, ansList);
                    SurveyVoteResponse voteResponse = new SurveyVoteResponse(response, voted);

                    await Clients.Caller.SendAsync("Load_survey", voteResponse);

                }
                catch (Exception e)
                {
                    await Clients.Caller.SendAsync("Load_survey_error");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }
        }

        // otrzymano zadanie wczytania wiadomosci
        public async Task LoadChatHistory(LoadChatRequest msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                //wczytanie wiadomosci starszych niz podana data
                try
                {
                    List<MessageResponse> messageResponse = new List<MessageResponse>();
                    IEnumerable<Message> messages = msgRepo.GetRoomMessages(msg.RoomId, msg.Date);

                    foreach (Message m in messages)
                    {
                        User creator = usrRepo.GetUser(m.AuthorId);

                        IEnumerable<Reaction> reactions = reactRepo.GetReactions(m.Id);
                        Dictionary<string, int> emojiList = new Dictionary<string, int>();
                        foreach (Reaction r in reactions)
                        {
                            try
                            {
                                emojiList.Add(r.Emoji, 1);
                            }
                            catch (ArgumentException)
                            {
                                emojiList[r.Emoji] += 1;
                            }

                        }

                        MessageResponse rsp = new MessageResponse(m.Id, creator.Username, m.SendTime, m.RoomId, m.ParentId, m.Content, emojiList);
                        messageResponse.Add(rsp);
                    }

                    LoadChatResponse response = new LoadChatResponse(msg.RoomId, messageResponse);
                    await Clients.Caller.SendAsync("Send_chat_history", response);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Clients.Caller.SendAsync("Load_chat_error");
                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            } 
        }

        // otrzymano zadanie otrzytania ankiet
        public async Task LoadSurveyHistory(LoadSurveyRequest msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                //wczytanie ankiet starszych niz podana data
                try
                {
                    List<SurveyVoteResponse> surveyList = new List<SurveyVoteResponse>();
                    IEnumerable<Survey> surveys = srvRepo.GetSurveys(msg.RoomId, msg.Date);

                    foreach (Survey s in surveys)
                    {
                        User usr = usrRepo.GetUser(s.AuthorId);

                        IEnumerable<Answer> answers = ansRepo.GetAnswers(s.Id);
                        List<SurveyAnswerResponse> ansList = new List<SurveyAnswerResponse>();

                        bool voted = false;

                        foreach (Answer ans in answers)
                        {

                            IEnumerable<Vote> votes = votRepo.GetVotes(ans.Id);
                            int amount = votes.Count();

                            SurveyAnswerResponse rsp = new SurveyAnswerResponse(ans.Id, ans.Content, amount);

                            ansList.Add(rsp);

                            Vote checkUser = votRepo.GetVote(ans.Id, usrId);
                            if (checkUser != null)
                            {
                                voted = true;
                            }
                        }
                        
                        SurveyResponse response = new SurveyResponse(s.Id, usr.Username, s.SendTime, s.RoomId, s.Question, ansList);
                        SurveyVoteResponse voteResponse = new SurveyVoteResponse(response, voted);
                        surveyList.Add(voteResponse);
                    }

                    LoadSurveyVoteResponse surveyResponse = new LoadSurveyVoteResponse(msg.RoomId, surveyList);
                    await Clients.Caller.SendAsync("Send_survey_history", surveyResponse);

                }
                catch (Exception e)
                {
                    await Clients.Caller.SendAsync("Load_survey_error");
                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }
        }

        // otrzymano zadanie wczytania wszystkiego z pokoju
        public async Task LoadAllHistory(LoadChatRequest msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                try
                {
                    //wczytanie wiadomosci
                    List<MessageResponse> messageResponse = new List<MessageResponse>();
                    IEnumerable<Message> messages = msgRepo.GetRoomMessages(msg.RoomId, msg.Date);

                    //znalezienie daty najstarszej wiadomości
                    DateTime oldestMsg = DateTime.Now;
                    foreach (Message m in messages)
                    {
                        if (m.SendTime < oldestMsg)
                        {
                            oldestMsg = m.SendTime;
                        }
                    }

                    foreach (Message m in messages)
                    {
                        User creator = usrRepo.GetUser(m.AuthorId);

                        IEnumerable<Reaction> reactions = reactRepo.GetReactions(m.Id);
                        Dictionary<string, int> emojiList = new Dictionary<string, int>();
                        foreach (Reaction r in reactions)
                        {
                            try
                            {
                                emojiList.Add(r.Emoji, 1);
                            }
                            catch (ArgumentException)
                            {
                                emojiList[r.Emoji] += 1;
                            }

                        }

                        MessageResponse rsp = new MessageResponse(m.Id, creator.Username, m.SendTime, m.RoomId, m.ParentId, m.Content, emojiList);
                        messageResponse.Add(rsp);
                    }

                    //znalezienie ankiet
                    List<SurveyVoteResponse> surveyList = new List<SurveyVoteResponse>();
                    IEnumerable<Survey> surveys = srvRepo.GetSurveys(msg.RoomId, msg.Date, oldestMsg);

                    foreach (Survey s in surveys)
                    {
                        User usr = usrRepo.GetUser(s.AuthorId);

                        IEnumerable<Answer> answers = ansRepo.GetAnswers(s.Id);
                        List<SurveyAnswerResponse> ansList = new List<SurveyAnswerResponse>();

                        bool voted = false;

                        foreach (Answer ans in answers)
                        {

                            IEnumerable<Vote> votes = votRepo.GetVotes(ans.Id);
                            int amount = votes.Count();

                            SurveyAnswerResponse rsp = new SurveyAnswerResponse(ans.Id, ans.Content, amount);

                            ansList.Add(rsp);

                            Vote checkUser = votRepo.GetVote(ans.Id, usrId);
                            if (checkUser != null)
                            {
                                voted = true;
                            }
                        }

                        SurveyResponse response = new SurveyResponse(s.Id, usr.Username, s.SendTime, s.RoomId, s.Question, ansList);
                        SurveyVoteResponse voteResponse = new SurveyVoteResponse(response, voted);
                        surveyList.Add(voteResponse);
                    }

                    LoadSurveyVoteResponse surveyResponse = new LoadSurveyVoteResponse(msg.RoomId, surveyList);
                    LoadAllResponse finalResponse = new LoadAllResponse(msg.RoomId, messageResponse, surveyResponse);
                    await Clients.Caller.SendAsync("Send_all_history", finalResponse);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Clients.Caller.SendAsync("Load_all_error");
                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }
        }

        // otrzymano ankiete (zawiera dopuszczalne odpowiedzi)
        public async Task SendChatSurvey(SurveyMessage msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                //sprawdzenie czy do pokoju mozna zapisywac
                if (CheckRoomWriteStatus(msg.RoomId))
                {
                    //utworzenie ankiety
                    try
                    {
                        Survey insertSrv = new Survey(Guid.NewGuid(), usrId, msg.RoomId, DateTime.Now, msg.Question);
                        Survey createdSrv = srvRepo.Insert(insertSrv);

                        await Clients.Caller.SendAsync("ACK");

                        //utworzenie dopuszczalnych odpowiedzi
                        foreach (string s in msg.AnswerList)
                        {
                            try
                            {
                                Answer insertAns = new Answer(Guid.NewGuid(), s, createdSrv.Id);
                                Answer createdAns = ansRepo.Insert(insertAns);

                            }
                            catch (Exception e)
                            {
                                await Clients.Caller.SendAsync("Server_survey_answer_error");
                            }

                        }

                        //wczytanie danych utworzonej ankiety i wyslanie do kazdego polaczonego klienta
                        IEnumerable<Answer> answers = ansRepo.GetAnswers(createdSrv.Id);
                        List<SurveyAnswerResponse> ansList = new List<SurveyAnswerResponse>();

                        foreach (Answer ans in answers)
                        {

                            IEnumerable<Vote> votes = votRepo.GetVotes(ans.Id);
                            int amount = votes.Count();

                            SurveyAnswerResponse rsp = new SurveyAnswerResponse(ans.Id, ans.Content, amount);

                            ansList.Add(rsp);
                        }

                        SurveyResponse response = new SurveyResponse(createdSrv.Id, usrName, createdSrv.SendTime, createdSrv.RoomId, createdSrv.Question, ansList);
                        SurveyVoteResponse voteResponse = new SurveyVoteResponse(response, false);

                        await Clients.All.SendAsync("Survey_created", voteResponse);

                    }
                    catch (Exception e)
                    {
                        await Clients.Caller.SendAsync("Server_survey_error");
                    }
                } else
                {
                    await Clients.Caller.SendAsync("Room_archived");
                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }
        }

        // otrzymano glos do ankiety
        public async Task SendChatSurveyVote(SurveyVoteMessage msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                //dodanie glosu pod warunkiem ze nie jest duplikatem
                try
                {
                    foreach (Guid ansId in msg.AnswerId)
                    {
                        Vote checkVote = votRepo.GetVote(ansId, usrId);
                        if (checkVote == null)
                        {
                            Vote insertVote = new Vote(Guid.NewGuid(), usrId, ansId);
                            Vote createdVote = votRepo.Insert(insertVote);

                            await Clients.Caller.SendAsync("ACK");

                        }
                        else
                        {
                            await Clients.Caller.SendAsync("Vote_duplicate", msg.SurveyId);
                        }

                    }

                    Survey srv = srvRepo.GetSurvey(msg.SurveyId);
                    User usr = usrRepo.GetUser(srv.AuthorId);

                    IEnumerable<Answer> answers = ansRepo.GetAnswers(msg.SurveyId);
                    List<SurveyAnswerResponse> ansList = new List<SurveyAnswerResponse>();

                    foreach (Answer ans in answers)
                    {

                        IEnumerable<Vote> votes = votRepo.GetVotes(ans.Id);
                        int amount = votes.Count();

                        SurveyAnswerResponse rsp = new SurveyAnswerResponse(ans.Id, ans.Content, amount);

                        ansList.Add(rsp);
                    }

                    SurveyResponse response = new SurveyResponse(srv.Id, usr.Username, srv.SendTime, srv.RoomId, srv.Question, ansList);

                    await Clients.All.SendAsync("Survey_updated", response);

                    SurveyVoteResponse voteResponse = new SurveyVoteResponse(response, true);
                    await Clients.Caller.SendAsync("Survey_voted", voteResponse);
                }
                catch (Exception e)
                {
                    await Clients.Caller.SendAsync("Vote_error");
                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }
        }

        // szukanie wiadomosci po ciagu znakow
        public async Task SearchMessage(SearchMessageRequest msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                try
                {

                    List<MessageResponse> messageResponse = new List<MessageResponse>();
                    IEnumerable<Message> messages = msgRepo.FindMessages(msg.RoomId, msg.Search);

                    foreach (Message m in messages)
                    {
                        User creator = usrRepo.GetUser(m.AuthorId);

                        IEnumerable<Reaction> reactions = reactRepo.GetReactions(m.Id);
                        Dictionary<string, int> emojiList = new Dictionary<string, int>();
                        foreach (Reaction r in reactions)
                        {
                            try
                            {
                                emojiList.Add(r.Emoji, 1);
                            }
                            catch (ArgumentException)
                            {
                                emojiList[r.Emoji] += 1;
                            }

                        }

                        MessageResponse rsp = new MessageResponse(m.Id, creator.Username, m.SendTime, m.RoomId, m.ParentId, m.Content, emojiList);
                        messageResponse.Add(rsp);
                    }

                    LoadChatResponse response = new LoadChatResponse(msg.RoomId, messageResponse);
                    await Clients.Caller.SendAsync("Send_search", response);

                }
                catch (Exception e)
                {
                    await Clients.Caller.SendAsync("Search_error");
                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }
        }

        // dodawanie rekacji
        public async Task AddReaction(ReactionMessage msg)
        {
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            if (CheckWhitelist(usrId, usrName))
            {
                try
                {
                    Reaction checkRection = reactRepo.GetReaction(usrId, msg.MessageId, msg.Emoji);

                    if (checkRection == null)
                    {
                        Reaction insert = new Reaction(Guid.NewGuid(), usrId, msg.MessageId, msg.Emoji);
                        Reaction created = reactRepo.InsertReaction(insert);

                        await Clients.Caller.SendAsync("ACK");
                    }
                    else
                    {
                        await Clients.Caller.SendAsync("React_duplicate", msg.MessageId);
                    }

                    Message message = msgRepo.GetMessage(msg.MessageId);
                    User creator = usrRepo.GetUser(message.AuthorId);

                    IEnumerable<Reaction> reactions = reactRepo.GetReactions(message.Id);
                    Dictionary<string, int> emojiList = new Dictionary<string, int>();
                    foreach (Reaction r in reactions)
                    {
                        try
                        {
                            emojiList.Add(r.Emoji, 1);
                        }
                        catch (ArgumentException)
                        {
                            emojiList[r.Emoji] += 1;
                        }

                    }

                    MessageResponse rsp = new MessageResponse(message.Id, creator.Username, message.SendTime, message.RoomId, message.ParentId, message.Content, emojiList);
                    LoadMessageResponse response = new LoadMessageResponse(rsp);

                    await Clients.All.SendAsync("Message_updated", response);

                }
                catch (Exception e)
                {
                    await Clients.Caller.SendAsync("React_error");
                }
            } else
            {
                await Clients.Caller.SendAsync("Not_allowed");
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _loginManager.Logout(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
