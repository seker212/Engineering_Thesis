using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ComeX.WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            for (int i = 0; i < 3; i++) {
                Button newBtn = new Button();

                newBtn.Content = i.ToString();
                newBtn.Name = "Button" + i.ToString();

                ServersWrapP.Children.Insert(0, newBtn);
            }

            
        }

        private void SendMessage(object sender, RoutedEventArgs e) {
            if (TypeTextBox.Text != "") {
                MessageUserControl newMessage = new MessageUserControl { };
                newMessage.AuthorText.Text = "Anonim";
                newMessage.DateText.Text = DateTime.Now.ToString();
                newMessage.ContentText.Text = TypeTextBox.Text;
                MessagesWrapP.Children.Add(newMessage);

                TypeTextBox.Text = "";
            }
        }

        private void AddMessagePlaceholder(object sender, RoutedEventArgs e) {
            TextBox textbox = sender as TextBox;
            if (textbox.Text == "")
                textbox.Text = "Type message here...";
        }

        private void RemoveMessagePlaceholder(object sender, RoutedEventArgs e) {
            TextBox textbox = sender as TextBox;
            if (textbox.Text == "Type message here...")
                textbox.Text = "";
        }
    }
}