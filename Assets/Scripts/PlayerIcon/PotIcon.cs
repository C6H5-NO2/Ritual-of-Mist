using ThisGame.Brew;
using UnityEngine;

namespace ThisGame.PlayerIcon {
    /// <summary> add to PotIcon </summary>
    public class PotIcon : MonoBehaviour {
        public Sprite empty, aqua, ingredient, boiling;
        private SpriteRenderer sr;


        public void SetState(PotState state) {
            // todo: update sr
        }


        private void Awake() {
            sr = GetComponent<SpriteRenderer>();
        }
    }
}
