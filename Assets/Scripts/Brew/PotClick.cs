using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.Brew {
    public class PotClick : MonoBehaviour, IPointerClickHandler {
        public BrewUI brewUI;

        public void OnPointerClick(PointerEventData eventData) {
            brewUI.gameObject.SetActive(!brewUI.gameObject.activeSelf);
        }
    }
}
