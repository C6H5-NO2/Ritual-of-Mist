using System;
using System.Collections;
using System.Collections.Generic;

namespace ThisGame.Utils {
    public class IdSoDict<T> : IEnumerable<T> where T : IdScriptableObject {
        public IdSoDict() {
            idDict = new Dictionary<uint, T>();
            nameidDict = new Dictionary<string, uint>();
        }

        public IdSoDict(int capacity) {
            idDict = new Dictionary<uint, T>(capacity);
            nameidDict = new Dictionary<string, uint>(capacity);
        }

        public IdSoDict(IReadOnlyCollection<T> array, bool sanCheck = false) {
            var capacity = array.Count;
            idDict = new Dictionary<uint, T>(capacity);
            nameidDict = new Dictionary<string, uint>(capacity);
            foreach(var o in array)
                Add(o, sanCheck);
        }

        public void Add(T so, bool sanCheck = false) {
            if(sanCheck)
                SanCheck(so);
            idDict.Add(so.id, so);
            nameidDict.Add(so.nameid, so.id);
        }

        public IEnumerator<T> GetEnumerator() => idDict.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool TryGetValue(uint id, out T value) => idDict.TryGetValue(id, out value);

        public bool TryGetValue(string nameid, out T value) {
            var flag = nameidDict.TryGetValue(nameid, out var id);
            if(flag)
                return TryGetValue(id, out value);
            value = null;
            return false;
        }

        public T this[uint id] => idDict[id];
        public T this[string nameid] => idDict[nameidDict[nameid]];

        public uint NameidToId(string nameid) => nameidDict[nameid];

        public bool ContinuityCheck(out uint idx) {
            idx = 0;
            for(var i = 1u; i <= idDict.Count; ++i)
                try {
                    var so = idDict[i];
                }
                catch(KeyNotFoundException) {
                    idx = i;
                    return false;
                }
            return true;
        }

        private void SanCheck(T so) {
            if(so.id == 0)
                throw new ArgumentException($"IDSO of name {so.name} contains reserved id 0");
            if(idDict.ContainsKey(so.id))
                throw new ArgumentException($"IDSO of name {so.name} contains duplicated id");
            if(nameidDict.ContainsKey(so.nameid))
                throw new ArgumentException($"IDSO of name {so.name} contains duplicated nameid");
        }

        private readonly Dictionary<uint, T> idDict;
        private readonly Dictionary<string, uint> nameidDict;
    }
}
