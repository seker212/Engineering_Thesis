using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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
    /// Logika interakcji dla klasy SettingsWindow.xaml
    /// </summary>
    public partial class SettingsView : Window {
        public SettingsView() {
            InitializeComponent();
        }

        private void OldPasswordChanged(object sender, RoutedEventArgs e) {
            if (this.DataContext != null) {
                ((dynamic)this.DataContext).OldPassword = ((PasswordBox)sender).SecurePassword;
            }
        }

        private void NewPasswordChanged(object sender, RoutedEventArgs e) {
            if (this.DataContext != null) {
                ((dynamic)this.DataContext).NewPassword = ((PasswordBox)sender).SecurePassword;
            }
        }

        private void AddOldPasswordPlaceholder(object sender, RoutedEventArgs e) {
            PasswordBox textbox = sender as PasswordBox;
            if (textbox.Password.Length == 0)
                OldPasswordPlaceholder.Visibility = Visibility.Visible;
        }

        private void RemoveOldPasswordPlaceholder(object sender, RoutedEventArgs e) {
            OldPasswordPlaceholder.Visibility = Visibility.Hidden;
        }

        private void AddNewPasswordPlaceholder(object sender, RoutedEventArgs e) {
            PasswordBox textbox = sender as PasswordBox;
            if (textbox.Password.Length == 0)
                NewPasswordPlaceholder.Visibility = Visibility.Visible;
        }

        private void RemoveNewPasswordPlaceholder(object sender, RoutedEventArgs e) {
            NewPasswordPlaceholder.Visibility = Visibility.Hidden;
        }
    }
}
