using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.MessageViewModels {
    public class ChatMessageViewModel : BaseMessageViewModel {
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

        public ChatMessageViewModel(MessageResponse message) {
            Message = message;
            SurveyAnswersVisibility = System.Windows.Visibility.Collapsed;
        }
    }
}
