using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComeX.WPF.ViewModels {
    public class BaseMessageViewModel : BaseViewModel {
        public Visibility SurveyAnswersVisibility { get; set; }

        public BaseMessageViewModel() {
            SurveyAnswersVisibility = Visibility.Collapsed;
        }
    }
}
