using UnityEngine;

namespace ThisGame.Utils {
    public class IdScriptableObject : ScriptableObject {
        /// <summary> start from 1 </summary>
        public uint id;
        public string nameid;
        public new string name;
        [TextArea] public string desc;
        public Sprite image;
    }
}
