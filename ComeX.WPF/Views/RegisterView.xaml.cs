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

namespace ComeX.WPF.Views {
    /// <summary>
    /// Logika interakcji dla klasy RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl {
        public RegisterView() {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e) {
            if (this.DataContext != null) {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }

        private void OnRetypePasswordChanged(object sender, RoutedEventArgs e) {
            if (this.DataContext != null) {
                ((dynamic)this.DataContext).RetypePassword = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}
