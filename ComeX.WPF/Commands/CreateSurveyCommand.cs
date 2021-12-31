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

        public CreateSurveyCommand(ChatViewModel viewModel) {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                SurveyMessage newSurvey = OpenSurveyWindow();
                if (newSurvey != null) {
                    newSurvey.Token = _viewModel.LoginDM.Username;

                    await _viewModel.CurrentServer.Service.SendChatSurvey(newSurvey);
                    _viewModel.ErrorMessage = string.Empty;
                }
            } catch (ArgumentException e) {
                _viewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Unable to create survey.";
            }
        }

        private SurveyMessage OpenSurveyWindow() {
            CreateSurveyWindow surveyWindow = new CreateSurveyWindow();
            SurveyMessage newSurvey;

            surveyWindow.Owner = Application.Current.MainWindow;
            surveyWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Application.Current.MainWindow.IsEnabled = false;

            surveyWindow.ShowDialog();
            newSurvey = surveyWindow.NewSurvey;

            return newSurvey;
        }
    }
}
