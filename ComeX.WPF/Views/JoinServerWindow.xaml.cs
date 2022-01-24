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

namespace ComeX.WPF.Views {
    /// <summary>
    /// Logika interakcji dla klasy JoinServerWindow.xaml
    /// </summary>
    public partial class JoinServerWindow : Window {
        public JoinServerWindow() {
            InitializeComponent();
            UrlTextBox.MaxLength = Consts.SURVEYQUESTION_MAXLEN;
        }

        private void AddUrlPlaceholder(object sender, RoutedEventArgs e) {
            TextBox textbox = sender as TextBox;
            if (textbox.Text == "")
                Placeholder.Visibility = Visibility.Visible;
        }

        private void RemoveUrlPlaceholder(object sender, RoutedEventArgs e) {
            Placeholder.Visibility = Visibility.Hidden;
        }

        private void CancelButtonHandler(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
