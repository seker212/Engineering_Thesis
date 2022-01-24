using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using ComeX.WPF.Commands;
using ComeX.WPF.Models;
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

        public string MessageDateTime {
            get {
                return Message.SendTime.ToString(Consts.DATEFORMAT);
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

        private List<ReactionMessageModel> _reactionsList;
        public List<ReactionMessageModel> ReactionList {
            get {
                return _reactionsList;
            }
            set {
                _reactionsList = value;
                OnPropertyChanged(nameof(ReactionList));
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

        private Visibility _toolsVisibility;
        public Visibility ToolsVisibility {
            get {
                return _toolsVisibility;
            }
            set {
                _toolsVisibility = value;
                OnPropertyChanged(nameof(ToolsVisibility));
            }
        }

        public ICommand SetReplyCommand { get; }
        public ICommand ReactionCommand { get; }

        public override Guid Id {
            get {
                return Message.Id;
            }
        }

        public override DateTime SendTime {
            get {
                return Message.SendTime;
            }
        }
        public ChatMessageViewModel() { }

        public ChatMessageViewModel(MessageResponse message, ChatViewModel chatVM, bool isRoomArchived, MessageResponse parentMessage = null) {
            SetReplyCommand = new SetReplyCommand(this, chatVM);
            ReactionCommand = new ReactionCommand(this, chatVM);
            if (isRoomArchived)
                ToolsVisibility = Visibility.Collapsed;
            else ToolsVisibility = Visibility.Visible;

            _chatVM = chatVM;
            Message = message;

            ReactionList = new List<ReactionMessageModel>();
 
            foreach (var react in Message.EmojiList) {
                string filename = "/Resources/Images/Emojis/" + react.Key + ".png";
                ReactionList.Add(new ReactionMessageModel(react.Key, filename, react.Value));
            }

            if (parentMessage != null) {
                AddParent(parentMessage);
            } else {
                ReplyParentVisibility = Visibility.Collapsed;
            }
        }

        public void AddParent(MessageResponse parentMessage) {
            ReplyParentVisibility = Visibility.Visible;
            ReplyParentId = parentMessage.Id;
            ReplyParentAuthor = parentMessage.Username;
            ReplyParentContent = parentMessage.Content;
        }
    }
}
