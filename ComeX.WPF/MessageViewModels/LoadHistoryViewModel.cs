using ComeX.WPF.Commands;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.MessageViewModels {
    public class LoadHistoryViewModel : BaseMessageViewModel {

        public ICommand LoadHistoryCommand { get; }

        public LoadHistoryViewModel(ChatViewModel chatViewModel) {
            LoadHistoryCommand = new LoadHistoryCommand(chatViewModel);
        }

        public override Guid Id => throw new NotImplementedException();

        public override DateTime SendTime => throw new NotImplementedException();
    }
}
