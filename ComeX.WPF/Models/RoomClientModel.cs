using ComeX.WPF.MessageViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.Models {
    public class RoomClientModel {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public bool IsArchived { get; set; }
        public ObservableCollection<BaseMessageViewModel> MessageList { get; set; }

        public RoomClientModel(Guid roomId, string name, bool isArchived) {
            RoomId = roomId;
            Name = name;
            IsArchived = isArchived;
        }
    }
}
