using UnityEngine;

namespace ThisGame.Utils {
    public class IdScriptableObject : ScriptableObject {
        public uint id;
        public string nameid;
        public new string name;
        [TextArea] public string desc;
        public Sprite image;
    }
}
