using ComeX.Lib.Common.ServerResponseModels;
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

        public RoomViewModel(ChatViewModel chatViewModel, Guid roomId, string name, bool isArchived) {
            RoomId = roomId;
            Name = name;
            IsArchived = isArchived;
            MessageList = new ObservableCollection<BaseMessageViewModel>();

            ChangeRoomCommand = new ChangeRoomCommand(chatViewModel, this);
        }

        public void AddMessages(List<MessageResponse> messageResponse, ChatViewModel chatViewModel) {
            messageResponse.Sort((p, q) => p.SendTime.CompareTo(q.SendTime));
            foreach (var message in messageResponse) {
                ChatMessageViewModel mewMessage = new ChatMessageViewModel(message, chatViewModel);
                if (!MessageList.Contains(mewMessage))
                    MessageList.Add(new ChatMessageViewModel(message, chatViewModel));
            }
            SortMessageList();
        }

        public void AddSurveys(List<SurveyResponse> surveyResponse, ChatViewModel chatViewModel) {
            surveyResponse.Sort((p, q) => p.SendTime.CompareTo(q.SendTime));
            foreach (var survey in surveyResponse) {
                MessageList.Add(new SurveyViewModel(survey, chatViewModel));
            }
            SortMessageList();
        }

        public void AddMessage(MessageResponse messageResponse, ChatViewModel chatViewModel) {
            ChatMessageViewModel newMessage = new ChatMessageViewModel(messageResponse, chatViewModel);
            ChatMessageViewModel oldMessage = GetMessageById(messageResponse.Id);

            if (oldMessage == null)
                MessageList.Add(newMessage);
            else {
                MessageList[MessageList.IndexOf(oldMessage)] = newMessage;
                SortMessageList();
            }

            chatViewModel.OnPropertyChanged(nameof(chatViewModel.CurrentRoomMessages));
        }

        public void AddSurvey(SurveyResponse surveyResponse, ChatViewModel chatViewModel) {
            SurveyViewModel newSurvey = new SurveyViewModel(surveyResponse, chatViewModel);
            SurveyViewModel oldSurvey = GetSurveyById(surveyResponse.Id);

            if (oldSurvey == null)
                MessageList.Add(newSurvey);
            else {
                MessageList[MessageList.IndexOf(oldSurvey)] = newSurvey;
                SortMessageList();
            }
        }

        // todo
        public void SortMessageList() {
            MessageList = new ObservableCollection<BaseMessageViewModel>(MessageList.OrderBy(i => i));
        }

        public ChatMessageViewModel GetMessageById(Guid id) {
            if (MessageList == null || MessageList.Count == 0) return null;
            var msg = (ChatMessageViewModel)MessageList.FirstOrDefault(o => ((ChatMessageViewModel)o).Message.Id == id);
            return msg;
        }

        public SurveyViewModel GetSurveyById(Guid id) {
            return (SurveyViewModel)MessageList.FirstOrDefault(o => ((SurveyViewModel)o).Survey.Id == id);
        }
    }
}
