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
    /// Logika interakcji dla klasy CreateSurveyAnswerUserControl.xaml
    /// </summary>
    public partial class CreateSurveyAnswerUserControl : UserControl {
        public CreateSurveyAnswerUserControl() {
            InitializeComponent();
        }

        private void AddAnswerPlaceholder(object sender, RoutedEventArgs e) {
            TextBox textbox = sender as TextBox;
            if (textbox.Text == "")
                Placeholder.Visibility = Visibility.Visible;
        }

        private void RemoveAnswerPlaceholder(object sender, RoutedEventArgs e) {
            Placeholder.Visibility = Visibility.Hidden;
        }

        public void DeleteAnswer (object sender, RoutedEventArgs e) {
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
