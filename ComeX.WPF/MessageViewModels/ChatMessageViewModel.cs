using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using ComeX.WPF.Commands;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.MessageViewModels {
    public class ChatMessageViewModel : BaseMessageViewModel {
        private ChatViewModel _chatVM;
        public MessageResponse Message { get; set; }

        public string MessageAuthor {
            get {
                return Message.Username;
            }
        }

        public string MessageDateTime { //TODO
            get {
                return "Today";
            }
        }

        public string MessageContent {
            get {
                return Message.Content;
            }
        }

        public ICommand SetReplyCommand { get; }

        public ChatMessageViewModel(MessageResponse message, ChatViewModel chatVM) {
            SetReplyCommand = new SetReplyCommand(this, chatVM);

            _chatVM = chatVM;
            Message = message;
        }
    }
}
