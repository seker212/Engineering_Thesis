using ComeX.Lib.Common.ServerResponseModels;
using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.Commands;
using ComeX.WPF.MessageViewModels;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.ViewModels {
    public class RoomViewModel {
        private ChatViewModel _chatViewModel;
        private ServerViewModel _serverViewModel;

        public Guid RoomId { get; set; }
        public bool IsArchived { get; set; }
        private string _name;
        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }
        public ObservableCollection<BaseMessageViewModel> MessageList { get; set; }

        public ICommand ChangeRoomCommand { get; }

        public RoomViewModel() { }

        public RoomViewModel(ChatViewModel chatViewModel, ServerViewModel serverViewModel, Guid roomId, string name, bool isArchived) {
            _chatViewModel = chatViewModel;
            _serverViewModel = serverViewModel;

            RoomId = roomId;
            Name = name;
            IsArchived = isArchived;
            MessageList = new ObservableCollection<BaseMessageViewModel>();

            ChangeRoomCommand = new ChangeRoomCommand(chatViewModel, this);
        }

        public void AddMessagesAndSurveys(List<MessageResponse> messageResponse, List<SurveyResponse> surveyResponse, ChatViewModel chatViewModel) {
            AddMessages(messageResponse, chatViewModel);
            AddSurveys(surveyResponse, chatViewModel);
            SortMessageList();
        }

        public void AddMessages(List<MessageResponse> messageResponse, ChatViewModel chatViewModel) {
            messageResponse.Sort((p, q) => p.SendTime.CompareTo(q.SendTime));
            foreach (var message in messageResponse) {
                if (GetMessageInListById(message.Id) == null)
                    AddMessage(message, chatViewModel);
            }
        }

        public void AddSurveys(List<SurveyResponse> surveyResponse, ChatViewModel chatViewModel) {
            surveyResponse.Sort((p, q) => p.SendTime.CompareTo(q.SendTime));
            foreach (var survey in surveyResponse) {
                if (GetMessageInListById(survey.Id) == null)
                    MessageList.Add(new SurveyViewModel(survey, chatViewModel, IsArchived));
            }
        }

        public async void AddMessage(MessageResponse messageResponse, ChatViewModel chatViewModel) {
            ChatMessageViewModel newMessage;
            if (messageResponse.ParentId == null) {
                newMessage = new ChatMessageViewModel(messageResponse, chatViewModel, IsArchived);
            } else {
                ChatMessageViewModel parent = ((ChatMessageViewModel)GetMessageInListById((Guid)messageResponse.ParentId));
                if (parent == null) {
                    await _serverViewModel.Service.LoadSpecificMessage(new LoadMessageRequest(_chatViewModel.LoginDM.Token, (Guid)messageResponse.ParentId));
                    MessageResponse tempParent = new MessageResponse();
                    tempParent.Id = (Guid)messageResponse.ParentId;
                    newMessage = new ChatMessageViewModel(messageResponse, chatViewModel, IsArchived, tempParent);
                } else {
                    newMessage = new ChatMessageViewModel(messageResponse, chatViewModel, IsArchived, parent.Message);
                }
            }

            ChatMessageViewModel oldMessage = (ChatMessageViewModel)GetMessageInListById(messageResponse.Id);

            if (oldMessage == null)
                MessageList.Add(newMessage);
            else {
                MessageList[MessageList.IndexOf(oldMessage)] = newMessage;              
            }

            SortMessageList();
        }

        public void UpdateParentMessage(MessageResponse messageResponse, ChatViewModel chatViewModel) {
            ChatMessageViewModel childMsg = GetMessageInListByParentId(messageResponse.Id);
            childMsg.AddParent(messageResponse);
            _chatViewModel.OnPropertyChanged(nameof(_chatViewModel.CurrentRoomMessages));
        }

        public void AddSurvey(SurveyResponse surveyResponse, ChatViewModel chatViewModel) {
            SurveyViewModel newSurvey = new SurveyViewModel(surveyResponse, chatViewModel, IsArchived);
            SurveyViewModel oldSurvey = (SurveyViewModel)GetMessageInListById(surveyResponse.Id);

            if (oldSurvey == null)
                MessageList.Add(newSurvey);
            else {
                MessageList[MessageList.IndexOf(oldSurvey)] = newSurvey;
            }

            SortMessageList();
        }

        public BaseMessageViewModel GetMessageInListById(Guid id) {
            if (MessageList == null || MessageList.Count == 0) return null;
            return MessageList.FirstOrDefault(i => i.Id == id);
        }

        public ChatMessageViewModel GetMessageInListByParentId(Guid id) {
            if (MessageList == null || MessageList.Count == 0) return null;
            return (ChatMessageViewModel)MessageList.FirstOrDefault(i => i.GetType() == typeof(ChatMessageViewModel) && ((ChatMessageViewModel)i).ReplyParentId == id);
        }

        public void SortMessageList() {
            MessageList = new ObservableCollection<BaseMessageViewModel>(MessageList.OrderBy(i => i.SendTime));
            _chatViewModel.OnPropertyChanged(nameof(_chatViewModel.CurrentRoomMessages));
        }

        public SurveyViewModel GetSurveyById(Guid id) {
            return (SurveyViewModel)MessageList.FirstOrDefault(o => ((SurveyViewModel)o).Survey.Id == id);
        }
    }
}
