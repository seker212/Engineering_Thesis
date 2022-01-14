using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.UserControls;
using ComeX.WPF.Windows;
using System;
using System.Collections.Generic;
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
        string SolutionPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        ImageBrush Avatar = new ImageBrush();

        public ChatView() {
            InitializeComponent();

            LoadUser();
            LoadServers();
            //LoadRooms();

        }

        void LoadUser() {
            // Avatar.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine(SolutionPath, @"ExampleImages\avatar2.jpg")));
            // UserAvatar.Background = Avatar;
        }

        void LoadServers() {

        }

        void LoadRooms() {
            for (int i = 0; i < 3; i++) {
                RoomUserControl newRoom = new RoomUserControl { };
                newRoom.RoomNameButton.Content = "Room" + i;
                newRoom.RoomNameButton.Click += new RoutedEventHandler(SwitchRoom);
                if (i == 1)
                    newRoom.SetNewMessage();
                RoomsWrapP.Children.Add(newRoom);
                if (i == 0) {
                    SwitchRoom(newRoom.RoomNameButton, null);
                }
            }
        }

        void SwitchServer(object sender, RoutedEventArgs e) { }

        void SwitchRoom(object sender, RoutedEventArgs e) {
            Button button = sender as Button;
            RoomUserControl room = button.DataContext as RoomUserControl;
            //SelectedRoomTitle.Text = (string)button.Content;
            room.DeleteNotify();
        }

        void AddServerMethod(object sender, RoutedEventArgs e) { }

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
