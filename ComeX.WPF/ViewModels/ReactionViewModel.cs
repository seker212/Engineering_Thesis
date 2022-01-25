using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.Commands;
using ComeX.WPF.MessageViewModels;
using ComeX.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.ViewModels {
    public class ReactionViewModel : BaseViewModel {
        private Visibility _windowVisibility;
        public Visibility WindowVisibility {
            get {
                return _windowVisibility;
            }
            set {
                _windowVisibility = value;
                OnPropertyChanged(nameof(WindowVisibility));
            }
        }

        private List<ReactionModel> _reactionsList;
        public List<ReactionModel> ReactionList {
            get {
                return _reactionsList;
            }
            set {
                _reactionsList = value;
                OnPropertyChanged(nameof(ReactionList));
            }
        }

        public object Visibility { get; internal set; }

        public string ChosenReaction {
            get {
                foreach (var r in ReactionList) {
                    if (r.IsChecked) {
                        return r.Name;
                    }
                }
                return string.Empty;
            }
        }

        public ICommand SendReactionCommand { get; }

        public ReactionViewModel(ChatViewModel chatViewModel, ChatMessageViewModel msgViewModel) {
            SendReactionCommand = new SendReactionCommand(chatViewModel, msgViewModel, this);

            ReactionList = new List<ReactionModel>();
            for (int i = 1; i < 8; i++) {
                ReactionList.Add(new ReactionModel(this, i.ToString(), "/Resources/Images/Emojis/" + i.ToString() + ".png"));
            }
        }

        public void UpdateChecks(string name) {
            foreach (var r in ReactionList) {
                if (r.Name != name)
                    r.IsChecked = false;
            }
            OnPropertyChanged(nameof(ReactionList));
        }
    }
}
