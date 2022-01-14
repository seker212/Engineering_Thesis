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
        private SurveyMessage _newSurvey;
        public SurveyMessage NewSurvey {
            get {
                return _newSurvey;
            }
            set {
                _newSurvey = value;
            }
        }

        public CreateSurveyWindow() {
            InitializeComponent();
            NewSurvey = null;
            QuestionTextBox.MaxLength = Consts.SURVEYQUESTION_MAXLEN;
        }

        private void AddSurveyButtonHandler(object sender, RoutedEventArgs e) {
            UnsetQuestionErrorMessage();
            UnsetAnswerErrorMessage();
            bool isInputCorrect = true;
            if (string.IsNullOrWhiteSpace(QuestionTextBox.Text)) {
                isInputCorrect = false;
                SetQuestionErrorMessage("Question is empty");
            }

            List<string> answerList = new List<string>();
            foreach (CreateSurveyAnswerUserControl answer in AnswersStackP.Children) {
                string answerText = answer.AnswerTextBox.Text;
                if (answerText != string.Empty)
                    answerList.Add(answerText);
            }
            if (answerList.Count == 0) {
                isInputCorrect = false;
                SetAnswerErrorMessage("Survey must contain at least one answer");
            }

            if (isInputCorrect) {
                SurveyMessage newSurvey = new SurveyMessage();
                newSurvey.IsMultipleChoice = (bool)MultipleChoiceCheckBox.IsChecked;
                newSurvey.Question = QuestionTextBox.Text;
                newSurvey.AnswerList = answerList;

                NewSurvey = newSurvey;
                this.Close();
            }
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

        public void SetQuestionErrorMessage(string errorMessage) {
            QuestionErrorLabel.Content = errorMessage;
            QuestionErrorLabel.Visibility = Visibility.Visible;
            QuestionTextBox.BorderThickness = new Thickness(2);
        }

        public void UnsetQuestionErrorMessage() {
            QuestionErrorLabel.Visibility = Visibility.Collapsed;
            QuestionTextBox.BorderThickness = new Thickness(0);
        }

        public void SetAnswerErrorMessage(string errorMessage) {
            AnswerErrorLabel.Content = errorMessage;
            AnswerErrorLabel.Visibility = Visibility.Visible;
        }

        public void UnsetAnswerErrorMessage() {
            AnswerErrorLabel.Visibility = Visibility.Collapsed;
        }
    }
}
