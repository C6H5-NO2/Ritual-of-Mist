using ThisGame.Brew;
using UnityEngine;

namespace ThisGame.PlayerIcon {
    /// <summary> add to PotIcon </summary>
    public class PotIcon : MonoBehaviour {
        public Sprite empty, aqua, ingredient, boiling;
        private SpriteRenderer sr;


        public void SetState(PotState state, bool isEmpty) {
            switch(state) {
                case PotState.BrewPrep:
                    sr.sprite = isEmpty ? aqua : ingredient;
                    break;
                case PotState.BrewPrg:
                    sr.sprite = boiling;
                    break;
                case PotState.CraftPrep:
                    sr.sprite = empty;
                    break;
            }
        }


        private void Awake() {
            sr = GetComponent<SpriteRenderer>();
        }
    }
}
