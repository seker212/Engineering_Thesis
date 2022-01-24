using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComeX.WPF.MessageViewModels {
    public class LoadingViewModel : BaseMessageViewModel {
        public LoadingViewModel() { }
        public override Guid Id => throw new NotImplementedException();

        public override DateTime SendTime => throw new NotImplementedException();

        private Visibility _loadingViewModel;
        public Visibility LoadingVisibility {
            get {
                return _loadingViewModel;
            }
            set {
                _loadingViewModel = value;
                OnPropertyChanged(nameof(LoadingVisibility));
            }
        }
    }
}
