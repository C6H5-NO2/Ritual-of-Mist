using System.Collections.Generic;

namespace ThisGame.Utils {
    public static class DictExt {
        // where TValue : IAdd<TValue>
        public static void EaddNset<TKey>(this Dictionary<TKey, uint> dict, TKey key, uint value) {
            if(value == 0)
                return;
            dict.TryGetValue(key, out var ori);
            dict[key] = ori + value;
        }
    }
}
