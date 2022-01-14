using ComeX.WPF.MessageViewModels;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class CheckedAnswerCommand : ICommand {
        private readonly SurveyViewModel _surveyViewModel;

        public CheckedAnswerCommand(SurveyViewModel surveyViewModel) {
            _surveyViewModel = surveyViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            _surveyViewModel.OnPropertyChanged(nameof(_surveyViewModel.ButtonEnabled));
        }
    }
}
