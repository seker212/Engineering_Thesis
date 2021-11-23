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
using ComeX.WPF.UserControls;

namespace ComeX.WPF.Windows {
    /// <summary>
    /// Logika interakcji dla klasy CreateSurveyWindow.xaml
    /// </summary>
    public partial class CreateSurveyWindow : Window {
        public CreateSurveyWindow() {
            InitializeComponent();
        }

        // TODO
        // private Survey CreateSurvey() {           
            // return survey obj
        // }

        private void AddSurveyButtonHandler(object sender, RoutedEventArgs e) {
            // Survey survey = CreateSurvey();

        }

        private void CancelButtonHandler (object sender, RoutedEventArgs e) {
            
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
