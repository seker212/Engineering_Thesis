using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.UserControls;
using ComeX.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
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

namespace ComeX.WPF.Views {
    /// <summary>
    /// Logika interakcji dla klasy ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl {
        public ChatView() {
            InitializeComponent();
        }

        private void AddMessagePlaceholder(object sender, RoutedEventArgs e) {
            TextBox textbox = sender as TextBox;
            if (textbox.Text == string.Empty)
                Placeholder.Visibility = Visibility.Visible;
        }

        private void RemoveMessagePlaceholder(object sender, RoutedEventArgs e) {
            Placeholder.Visibility = Visibility.Hidden;
        }
    }
}
