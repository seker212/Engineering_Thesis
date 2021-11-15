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

namespace ComeX.WPF {
    /// <summary>
    /// Logika interakcji dla klasy MessageUserControl.xaml
    /// </summary>
    public partial class MessageUserControl : UserControl {
        public MessageUserControl() {
            InitializeComponent();
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(string), typeof(MessageUserControl));
        public static readonly DependencyProperty AuthorProperty = DependencyProperty.Register("Author", typeof(string), typeof(MessageUserControl));
        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(string), typeof(MessageUserControl));

    }
}
