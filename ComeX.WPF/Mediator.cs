using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF {
    public static class Mediator {
        private static IDictionary<string, List<Action<object>>> dict =
           new Dictionary<string, List<Action<object>>>();

        public static void Subscribe(string token, Action<object> callback) {
            if (!dict.ContainsKey(token)) {
                var list = new List<Action<object>>();
                list.Add(callback);
                dict.Add(token, list);
            } else {
                bool found = false;
                foreach (var item in dict[token])
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                if (!found)
                    dict[token].Add(callback);
            }
        }

        public static void Unsubscribe(string token, Action<object> callback) {
            if (dict.ContainsKey(token))
                dict[token].Remove(callback);
        }

        public static void Notify(string token, object args = null) {
            if (dict.ContainsKey(token))
                foreach (var callback in dict[token])
                    callback(args);
        }
    }
}
