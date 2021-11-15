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
    /// Logika interakcji dla klasy RoomUserControl.xaml
    /// </summary>
    public partial class RoomUserControl : UserControl {
        public RoomUserControl() {
            InitializeComponent();
        }

        public void SetNewMessage() {
            this.NotifyRoomEllipse.Style = (Style)Room.FindResource("NewMessageRoomStyle");
        }

        public void SetNewMention() {
            this.NotifyRoomEllipse.Style = (Style)Room.FindResource("NewMentionRoomStyle");
        }

        public void DeleteNotify() {
            this.NotifyRoomEllipse.Style = (Style)Room.FindResource("DefaultEllipseStyle");
        }
    }
}
