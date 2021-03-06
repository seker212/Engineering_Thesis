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
using System.Windows.Controls;
using System.Windows.Input;

namespace ComeX.WPF.MessageViewModels {
    public class SurveyViewModel : BaseMessageViewModel {
        private ChatViewModel _chatVM;

        private SurveyResponse _survey;
        public SurveyResponse Survey {
            get {
                return _survey;
            }
            set {
                _survey = value;
                OnPropertyChanged(nameof(Survey));
                OnPropertyChanged(nameof(Survey));
            }
        }

        public string MessageAuthor {
            get {
                return Survey.Username;
            }
        }

        public string MessageDateTime {
            get {
                return Survey.SendTime.ToString(Consts.DATEFORMAT);
            }
        }

        public override Guid Id {
            get {
                return Survey.Id;
            }
        }

        public override DateTime SendTime {
            get {
                return Survey.SendTime;
            }
        }

        public string MessageContent {
            get {
                return Survey.Question;
            }
        }

        public List<SurveyAnswerViewModel> SurveyAnswers { get; set; }

        private bool _isRoomArchived;
        public bool IsRoomArchived {
            get {
                return _isRoomArchived;
            }
            set {
                _isRoomArchived = value;
                OnPropertyChanged(nameof(IsRoomArchived));
            }
        }

        public Visibility ButtonVisibility {
            get {
                return IsRoomArchived ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public bool ButtonEnabled {
            get {
                return (AnyAnswerChecked() && !Voted);
            }
        }

        private bool _voted;
        public bool Voted {
            get {
                return _voted;
            }
            set {
                _voted = value;
                if (value) {
                    ButtonContent = "Already voted";
                    foreach (var ans in SurveyAnswers)
                        ans.AlreadyVoted = true;
                } else ButtonContent = "Vote";
                OnPropertyChanged(nameof(Voted));
            }
        }

        private string _buttonContent;
        public string ButtonContent {
            get {
                return _buttonContent;
            }
            set {
                _buttonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
            }
        }

        public ICommand SendSurveyVoteCommand { get; }
        public ICommand CheckedAnswerCommand { get; }

        public SurveyViewModel(SurveyResponse survey, ChatViewModel chatVM, bool isRoomArchived) {
            SendSurveyVoteCommand = new SendSurveyVoteCommand(this, chatVM);
            CheckedAnswerCommand = new CheckedAnswerCommand(this);
            IsRoomArchived = isRoomArchived;
            Voted = false;

            _chatVM = chatVM;
            Survey = survey;

            SurveyAnswers = new List<SurveyAnswerViewModel>();
            foreach (var answer in Survey.AnswerList) {
                SurveyAnswers.Add(new SurveyAnswerViewModel(answer, false, IsRoomArchived));
            }
        }

        public List<SurveyAnswerResponse> GetCheckedAnswers() {
            List<SurveyAnswerResponse> checkedAnswers = new List<SurveyAnswerResponse>();
            foreach (var answer in SurveyAnswers) {
                if (answer.IsChecked) checkedAnswers.Add(answer.AnswerResponse);
            }
            return checkedAnswers;
        }

        private bool AnyAnswerChecked() {
            return GetCheckedAnswers().Count > 0;
        }
    }
}
