using ThisGame.Utils;
using UnityEngine;

namespace ThisGame.Letter {
    [CreateAssetMenu(fileName = "Letter", menuName = "Letter/Letter Data")]
    public class LetterData : IdScriptableObject {
        // todo: write to file
        public bool Received { get; set; }
        public bool Read { get; set; }
    }
}
