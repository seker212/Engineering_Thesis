using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.UserControls;

namespace ComeX.WPF.Windows {
    /// <summary>
    /// Logika interakcji dla klasy CreateSurveyWindow.xaml
    /// </summary>
    public partial class CreateSurveyWindow : Window {
        private Survey _newSurvey;
        public Survey NewSurvey {
            get {
                return _newSurvey;
            }
            set {
                _newSurvey = value;
            }
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage {
            get {
                return _errorMessage;
            }
            set {
                _errorMessage = value;
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public CreateSurveyWindow() {
            InitializeComponent();
            NewSurvey = null;
        }

        private void AddSurveyButtonHandler(object sender, RoutedEventArgs e) {
            if (string.IsNullOrWhiteSpace(QuestionTextBox.Text)) {
                ErrorMessage = "Question is empty";
            }
            List<SurveyAnswer> answerList = new List<SurveyAnswer>();
            foreach (CreateSurveyAnswerUserControl answer in AnswersStackP.Children) {
                string answerText = answer.AnswerTextBox.Text;
                if (answerText != string.Empty)
                    answerList.Add(new SurveyAnswer() { Content = answerText });
            }
            if (answerList.Count == 0) {
                ErrorMessage = "Survey must contain at least one answer";
            }

            if (!HasErrorMessage) {
                Survey newSurvey = new Survey();
                newSurvey.IsMultipleChoice = (bool)MultipleChoiceCheckBox.IsChecked;
                newSurvey.Question = QuestionTextBox.Text;
                newSurvey.AnswerList = answerList;

                NewSurvey = newSurvey;
            }
            this.Close();
        }

        private void AddQuestionPlaceholder(object sender, RoutedEventArgs e) {
            TextBox textbox = sender as TextBox;
            if (textbox.Text == "")
                Placeholder.Visibility = Visibility.Visible;
        }

        private void RemoveQuestionPlaceholder(object sender, RoutedEventArgs e) {
            Placeholder.Visibility = Visibility.Hidden;
        }

        private void AddAnswer(object sender, RoutedEventArgs e) {
            AnswersStackP.Children.Add(new CreateSurveyAnswerUserControl { });
        }
    }
}
