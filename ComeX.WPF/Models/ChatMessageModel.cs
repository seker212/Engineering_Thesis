using ComeX.Lib.Common.ServerCommunicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.Models {
    public class ChatMessageModel : BaseModel {
        public Message Message { get; set; }

        public string MessageContent {
            get {
                return Message.Content;
            }
        }
        public string MessageAuthor {
            get {
                return Message.Token;
            }
        }

        public ChatMessageModel(Message message) {
            Message = message;
            //MessageContent = Message.Content;
            //MessageAuthor = Message.Token;
        }
    }
}
