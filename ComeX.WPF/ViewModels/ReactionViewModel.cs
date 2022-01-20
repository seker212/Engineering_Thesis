using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.Commands;
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
        private ReactionMessage _reaction;
        public ReactionMessage Reaction {
            get {
                return _reaction;
            }
            set {
                _reaction = value;
            }
        }

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

        public ICommand ChooseReactionCommand { get; }

        public ReactionViewModel(ChatViewModel chatViewModel) {
            ChooseReactionCommand = new ChooseReactionCommand(chatViewModel, this);

            Reaction = null;
            ReactionList = new List<ReactionModel>();
            for (int i = 1; i < 8; i++) {
                ReactionList.Add(new ReactionModel(i.ToString(), "/Resources/Images/Emojis/" + i.ToString() + ".png"));
            }
        }
    }
}
