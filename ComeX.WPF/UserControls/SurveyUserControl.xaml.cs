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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComeX.WPF.UserControls {
    /// <summary>
    /// Logika interakcji dla klasy SurveyUserControl.xaml
    /// </summary>
    public partial class SurveyUserControl : UserControl {
        public List<string> CheckedAnswers;
        public SurveyUserControl() {
            InitializeComponent();
            CheckedAnswers = new List<string>();
        }

        private void AnswerCheckBox_Checked(object sender, RoutedEventArgs e) {
            CheckedAnswers.Add(((CheckBox)sender).Content.ToString());
        }

        private void AnswerCheckBox_Unchecked(object sender, RoutedEventArgs e) {
            CheckedAnswers.Remove(((CheckBox)sender).Content.ToString());
        }
    }
}
