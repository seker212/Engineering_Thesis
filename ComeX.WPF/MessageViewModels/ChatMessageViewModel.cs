using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using ComeX.WPF.Commands;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private Guid _replyParentId;
        public Guid ReplyParentId {
            get {
                return _replyParentId;
            }
            set {
                _replyParentId = value;
                if (value == Guid.Empty) {
                    ReplyParentAuthor = string.Empty;
                    ReplyParentContent = string.Empty;
                    ReplyParentVisibility = Visibility.Collapsed;
                }
            }
        }

        private string _replyParentAuthor;
        public string ReplyParentAuthor {
            get {
                return _replyParentAuthor;
            }
            set {
                _replyParentAuthor = value;
            }
        }

        private string _replyParentContent;
        public string ReplyParentContent {
            get {
                return _replyParentContent;
            }
            set {
                _replyParentContent = value;
            }
        }
        public string ReplyParentContentPrint {
            get {
                if (ReplyParentContent == null || ReplyParentContent.Length < 21) return ReplyParentContent;
                else return ReplyParentContent.Substring(0, 20) + "...";
            }
        }

        private Visibility _replyParentVisibility;
        public Visibility ReplyParentVisibility {
            get {
                return _replyParentVisibility;
            }
            set {
                _replyParentVisibility = value;
                OnPropertyChanged(nameof(ReplyParentVisibility));
            }
        }

        public ICommand SetReplyCommand { get; }

        public ChatMessageViewModel(MessageResponse message, ChatViewModel chatVM, MessageResponse parentMessage = null) {
            SetReplyCommand = new SetReplyCommand(this, chatVM);

            _chatVM = chatVM;
            Message = message;

            if (parentMessage != null) {
                ReplyParentVisibility = Visibility.Visible;
                ReplyParentId = parentMessage.Id;
                ReplyParentAuthor = parentMessage.Username;
                ReplyParentContent = parentMessage.Content;
            } else {
                ReplyParentVisibility = Visibility.Collapsed;
            }
        }
    }
}
