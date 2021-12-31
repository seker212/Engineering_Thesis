﻿using System;
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
    /// Logika interakcji dla klasy ServerUserControl.xaml
    /// </summary>
    public partial class ServerUserControl : UserControl {
        public ServerUserControl() {
            InitializeComponent();
        }


        public string RoomName { get; set; }

        public void SetNewMessage() {
            this.NotifyServerEllipse.Style = (Style)Server.FindResource("NewMessageRoomStyle");
        }

        public void SetNewMention() {
            this.NotifyServerEllipse.Style = (Style)Server.FindResource("NewMentionRoomStyle");
        }

        public void DeleteNotify() {
            this.NotifyServerEllipse.Style = (Style)Server.FindResource("DefaultEllipseStyle");
        }
    }
}