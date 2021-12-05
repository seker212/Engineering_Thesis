using ComeX.Lib.Common.ServerCommunicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.ViewModels {
    public class ChatMessageViewModel : BaseMessageViewModel {
        public Message Message { get; set; }

        public string MessageAuthor {
            get {
                return Message.Token;
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

        public ChatMessageViewModel(Message message) {
            Message = message;
            SurveyAnswersVisibility = System.Windows.Visibility.Collapsed;
        }
    }
}
