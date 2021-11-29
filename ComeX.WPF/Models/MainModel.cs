using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.Models {
    public class MainModel {
        public ChatModel ChatModel { get; }

        public MainModel(ChatModel chatModel) {
            ChatModel = chatModel;
        }
    }
}
