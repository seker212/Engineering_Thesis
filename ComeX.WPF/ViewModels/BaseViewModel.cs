using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.ViewModels {
    public class BaseViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string SecureStringToString(SecureString value) {
            IntPtr pointer = IntPtr.Zero;
            try {
                pointer = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(pointer);
            } finally {
                Marshal.ZeroFreeGlobalAllocUnicode(pointer);
            }
        }
    }
}
