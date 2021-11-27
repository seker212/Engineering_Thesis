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
using ComeX.WPF.UserControls;

namespace ComeX.WPF {
    /// <summary>
    /// Logika interakcji dla klasy MessageTemplateUserControl.xaml
    /// </summary>
    public partial class MessageTemplateUserControl : UserControl {
        public MessageTemplateUserControl() {
            InitializeComponent();
        }

        public MessageTemplateUserControl(string author, ImageBrush Avatar, MessageUserControl content) {
            InitializeComponent();
            AuthorText.Text = author;
            MessageAvatar.Background = Avatar;
            DateText.Text = DateTime.Now.ToString();
            AddContent(content);
        }

        public MessageTemplateUserControl(string author, ImageBrush Avatar, SurveyUserControl content) {
            InitializeComponent();
            AuthorText.Text = author;
            MessageAvatar.Background = Avatar;
            DateText.Text = DateTime.Now.ToString();
            AddContent(content);
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(MessageUserControl), typeof(MessageTemplateUserControl));
        public static readonly DependencyProperty AuthorProperty = DependencyProperty.Register("Author", typeof(string), typeof(MessageTemplateUserControl));
        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(string), typeof(MessageTemplateUserControl));

        public void AddContent(MessageUserControl messageUC) {
            ContentGrid.Children.Add(messageUC);
        }

        public void AddContent(SurveyUserControl messageUC) {
            ContentGrid.Children.Add(messageUC);
        }
    }
}
