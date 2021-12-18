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

namespace ComeX.WPF.Windows {
    /// <summary>
    /// Logika interakcji dla klasy SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window {
        private OptionEnum _option;
        public OptionEnum Option {
            get {
                return _option;
            }
            set {
                _option = value;
            }
        }

        public SettingsWindow() {
            InitializeComponent();
        }

        private void LogoutButtonHandler(object sender, RoutedEventArgs e) {
            // ask if you really want to log out
            Option = OptionEnum.Logout;
            this.Close();
        }
    }
}
