using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.ViewModels {
    public class MainViewModel {
        public ChatViewModel ChatViewModel { get; }

        public MainViewModel(ChatViewModel chatViewModel) {
            ChatViewModel = chatViewModel;
        }
    }
}
