using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.Models {
    public class ReactionModel {
        private ReactionViewModel _reactVM;
        public string Name { get; set; }
        public string Filename { get; set; }

        private bool _isChecked;
        public bool IsChecked {
            get {
                return _isChecked;
            }
            set {
                _isChecked = value;
                if (value)
                    _reactVM.UpdateChecks(Name);
            }
        }

        public ReactionModel(ReactionViewModel reactVM, string name, string filename) {
            _reactVM = reactVM;
            Name = name;
            Filename = filename;
        }
    }
}
