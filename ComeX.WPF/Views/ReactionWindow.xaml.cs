using ComeX.Lib.Common.ServerCommunicationModels;
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
    /// Logika interakcji dla klasy ReactionWindow.xaml
    /// </summary>
    public partial class ReactionWindow : Window {
        private ReactionMessage _reaction;
        public ReactionMessage Reaction {
            get {
                return _reaction;
            }
            set {
                _reaction = value;
            }
        }

        public ReactionWindow() {
            InitializeComponent();
            Reaction = null;
        }

        protected override void OnDeactivated(EventArgs e) {
            base.OnDeactivated(e);
            this.Close();
        }

        private void AddReactionButtonHandler(object sender, RoutedEventArgs e) {
            string buttonName = ((Button)sender).Content.ToString();
            if (this.DataContext != null) {
                ((dynamic)this.DataContext).ChooseReactionCommand.Execute(buttonName);
            }
        }
    }
}
