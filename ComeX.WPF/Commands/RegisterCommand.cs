using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class RegisterCommand : ICommand {
        private readonly RegisterViewModel _viewModel;
        private readonly RegisterService _registerService;

        public RegisterCommand(RegisterViewModel viewModel, RegisterService registerService) {
            _viewModel = viewModel;
            _registerService = registerService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                _viewModel.UnsetUsernameErrorMessage();
                _viewModel.UnsetPasswordErrorMessage();
                _viewModel.UnsetRetypePasswordErrorMessage();

                bool isInputCorrect = true;
                string login = _viewModel.Username;
                SecureString password = _viewModel.Password;
                SecureString retypePassword = _viewModel.RetypePassword;

                if (string.IsNullOrWhiteSpace(login)) {
                    _viewModel.SetUsernameErrorMessage("Username cannot be empty");
                    isInputCorrect = false;
                }
                if (password == null || password.Length == 0) {
                    _viewModel.SetPasswordErrorMessage("Password cannot be empty");
                    isInputCorrect = false;
                }
                if (retypePassword == null || retypePassword.Length == 0) {
                    _viewModel.SetRetypePasswordErrorMessage("Password cannot be empty");
                    isInputCorrect = false;
                }
                if (!IsPasswordFormatValid()) {
                    _viewModel.SetPasswordErrorMessage("Password must be at least 8 characters long"); // TODO message with more conditions
                    isInputCorrect = false;
                }
                else if (!ArePasswordsEqual()) {
                    _viewModel.SetRetypePasswordErrorMessage("Passwords are not equal");
                    isInputCorrect = false;
                }
                if (isInputCorrect) {
                    //Login login = new Login(login, password);
                    //await _chatService.SendChatMessage(message);
                    //_viewModel.ErrorMessage = string.Empty;
                    //_viewModel.Content = string.Empty;
                }
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Unable to send message.";
            }
        }

        private bool ArePasswordsEqual() {
            SecureString password1 = _viewModel.Password;
            SecureString password2 = _viewModel.RetypePassword;

            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;
            try {
                bstr1 = Marshal.SecureStringToBSTR(password1);
                bstr2 = Marshal.SecureStringToBSTR(password2);

                int length1 = Marshal.ReadInt32(bstr1, -4);
                int length2 = Marshal.ReadInt32(bstr2, -4);
                if (length1 == length2) {
                    for (int i = 0; i < length1; ++i) {
                        byte b1 = Marshal.ReadByte(bstr1, i);
                        byte b2 = Marshal.ReadByte(bstr2, i);
                        if (b1 != b2) {
                            return false;
                        }
                    }
                } else {
                    return false;
                }
                return true;
            } finally {
                if (bstr2 != IntPtr.Zero) {
                    Marshal.ZeroFreeBSTR(bstr2);
                }
                if (bstr1 != IntPtr.Zero) {
                    Marshal.ZeroFreeBSTR(bstr1);
                }
            }
        }

        private bool IsPasswordFormatValid() { // TODO more conditions
            return (_viewModel.Password.Length > 7);
        }
    }
}
