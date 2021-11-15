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
using System.IO;
using ComeX.WPF.UserControls;

namespace ComeX.WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        string SolutionPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        ImageBrush Avatar = new ImageBrush();

        public MainWindow() {
            InitializeComponent();

            LoadUser();
            LoadServers();
            LoadRooms();    
        }

        void LoadUser() {
            Avatar.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine(SolutionPath, @"ExampleImages\avatar.jpg")));
            UserAvatar.Background = Avatar;
        }

        void LoadServers() {
            for (int i = 0; i < 3; i++) {
                
            }
        }

        void LoadRooms() {
            for (int i = 0; i < 3; i++) {
                RoomUserControl newRoom = new RoomUserControl { };
                newRoom.RoomNameButton.Content = "Room" + i;
                newRoom.RoomNameButton.Click += new RoutedEventHandler(SwitchRoom);
                if (i == 1)
                    newRoom.SetNewMessage();
                if (i == 2)
                    newRoom.SetNewMention();
                RoomsWrapP.Children.Add(newRoom);
            }
        }

        void SwitchServer(object sender, RoutedEventArgs e) { }

        void SwitchRoom(object sender, RoutedEventArgs e) {
            Button button = sender as Button;
            RoomUserControl room = button.DataContext as RoomUserControl;
            SelectedRoomTitle.Text = (string)button.Content;
            SelectedRoomDescription.Text = "Opis pokoju " + (string)button.Content;
            room.DeleteNotify();
        }

        void AddServerMethod(object sender, RoutedEventArgs e) { }

        private void SendMessage(object sender, RoutedEventArgs e) {
            if (TypeTextBox.Text != "") {
                MessageUserControl newMessage = new MessageUserControl { };
                newMessage.AuthorText.Text = "Anonim";
                newMessage.MessageAvatar.Background = Avatar;
                newMessage.DateText.Text = DateTime.Now.ToString();
                newMessage.ContentText.Text = TypeTextBox.Text;
                MessagesWrapP.Children.Add(newMessage);

                TypeTextBox.Text = "";
            }
        }

        private void AddMessagePlaceholder(object sender, RoutedEventArgs e) {
            TextBox textbox = sender as TextBox;
            Placeholder.Visibility = Visibility.Visible;
        }

        private void RemoveMessagePlaceholder(object sender, RoutedEventArgs e) {
            TextBox textbox = sender as TextBox;
            Placeholder.Visibility = Visibility.Hidden;
        }
    }
}