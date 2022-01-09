using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.Models {
    public class ReactionMessageModel {
        public string Name { get; set; }
        public string Filename { get; set; }
        public int Counter { get; set; }

        public ReactionMessageModel(string name, string filename, int counter) {
            Name = name;
            Filename = filename;
            Counter = counter;
        }
    }
}
