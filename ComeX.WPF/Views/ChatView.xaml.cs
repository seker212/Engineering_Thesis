﻿using ComeX.WPF.UserControls;
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
            LoadRooms();

            //TEST_AddSurvey();
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
                else if (i == 2)
                    newRoom.SetNewMention();
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
            SelectedRoomTitle.Text = (string)button.Content;
            SelectedRoomDescription.Text = "Opis pokoju " + (string)button.Content;
            room.DeleteNotify();
        }

        void AddServerMethod(object sender, RoutedEventArgs e) { }

        private void SendMessage(object sender, RoutedEventArgs e) {
            if (TypeTextBox.Text != "") {
                MessageUserControl newMessageContent = new MessageUserControl { };
                newMessageContent.ContentText.Text = TypeTextBox.Text;

                MessageTemplateUserControl newMessage = new MessageTemplateUserControl("Anonim", Avatar, newMessageContent) { };

                //newMessage.Content = newMessageContent;


                TypeTextBox.Text = "";
            }
        }

        private void CreateSurvey(object sender, RoutedEventArgs e) {
            CreateSurveyWindow surveyWindow = new CreateSurveyWindow();

            surveyWindow.Owner = Application.Current.MainWindow;
            surveyWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Application.Current.MainWindow.IsEnabled = false;

            surveyWindow.ShowDialog();
        }

        private void TEST_AddSurvey() {
            SurveyUserControl newSurvey = new SurveyUserControl { };
            newSurvey.ContentText.Text = "Question here";

            SurveyAnswerUserControl newAnswer = new SurveyAnswerUserControl { };
            newAnswer.AnswerText.Text = "new answer";
            newAnswer.AnswerVotesCounter.Text = "10";

            Button newButton = new Button();
            newButton.Content = "Add";

            newSurvey.AnswersWrapP.Children.Add(newAnswer);
            newSurvey.AnswersWrapP.Children.Add(newButton);

            MessageTemplateUserControl newMessage = new MessageTemplateUserControl("Anonim", Avatar, newSurvey) { };

            //MessagesWrapP.Children.Add(newMessage);
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
