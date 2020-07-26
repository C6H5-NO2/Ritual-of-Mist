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

        //public T this[uint id] {
        //    get {
        //        idDict.TryGetValue(id, out var so);
        //        // null UnityObject ? real null
        //        return so == null ? null : so;
        //    }
        //}

        //public T this[string nameid] {
        //    get {
        //        nameidDict.TryGetValue(nameid, out var id);
        //        return id == 0 ? null : this[id];
        //    }
        //}

        public T this[uint id] => idDict[id];
        public T this[string nameid] => idDict[nameidDict[nameid]];

        private void SanCheck(T so) {
            if(idDict.ContainsKey(so.id))
                throw new ArgumentException($"IDSO of name {so.name} contains duplicated id");
            if(nameidDict.ContainsKey(so.nameid))
                throw new ArgumentException($"IDSO of name {so.name} contains duplicated nameid");
        }

        private readonly Dictionary<uint, T> idDict;
        private readonly Dictionary<string, uint> nameidDict;
    }
}
