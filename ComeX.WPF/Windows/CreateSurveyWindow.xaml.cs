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
        public CreateSurveyWindow() {
            InitializeComponent();
        }

        private void AddSurveyButtonHandler(object sender, RoutedEventArgs e) {
            List<SurveyAnswer> answerList = new List<SurveyAnswer>();
            foreach (CreateSurveyAnswerUserControl answer in AnswersStackP.Children) {
                string answerText = answer.AnswerTextBox.Text;
                if (answerText != string.Empty)
                    answerList.Add(new SurveyAnswer() { Content = answerText });
            }

            Survey newSurvey = new Survey();
            newSurvey.IsMultipleChoice = (bool)MultipleChoiceCheckBox.IsChecked;
            newSurvey.Question = QuestionTextBox.Text;
            newSurvey.AnswerList = answerList;

            // return newSurvey;
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
