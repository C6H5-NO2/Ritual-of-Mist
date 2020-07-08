﻿using UnityEngine;

namespace ThisGame.Utils {
    public class IdScriptableObject : ScriptableObject {
        public ushort id;
        public string nameid;
        public new string name;
        [TextArea] public string desc;
        public Sprite image;
    }
}
