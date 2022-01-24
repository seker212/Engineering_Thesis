using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.Models {
    public class ReactionModel {
        public string Name { get; set; }
        public string Filename { get; set; }

        public ReactionModel(string name, string filename) {
            Name = name;
            Filename = filename;
        }
    }
}
