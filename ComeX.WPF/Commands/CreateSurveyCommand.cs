using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using ComeX.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class CreateSurveyCommand : ICommand {
        private readonly ChatViewModel _viewModel;
        private readonly SignalRChatService _service;

        public CreateSurveyCommand(ChatViewModel viewModel, SignalRChatService service) {
            _viewModel = viewModel;
            _service = service;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                Survey newSurvey = OpenSurveyWindow();
                if (newSurvey != null) {
                    await _service.SendChatSurvey(newSurvey);
                    _viewModel.ErrorMessage = string.Empty;
                }
            } catch (ArgumentException e) {
                _viewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Unable to create survey.";
            }
        }

        private Survey OpenSurveyWindow() {
            CreateSurveyWindow surveyWindow = new CreateSurveyWindow();
            Survey newSurvey;

            surveyWindow.Owner = Application.Current.MainWindow;
            surveyWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Application.Current.MainWindow.IsEnabled = false;

            surveyWindow.ShowDialog();
            newSurvey = surveyWindow.NewSurvey;

            return newSurvey;
        }
    }
}
