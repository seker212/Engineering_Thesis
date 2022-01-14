using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComeX.WPF.MessageViewModels {
    public abstract class BaseMessageViewModel : BaseViewModel {
        public abstract Guid Id { get; }
        public abstract DateTime SendTime { get; }

        public BaseMessageViewModel() { }
    }
}
