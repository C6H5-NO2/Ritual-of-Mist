using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.GeneralUI {
    public class ClickShow : MonoBehaviour, IPointerClickHandler {
        public Transform target;

        public void OnPointerClick(PointerEventData eventData) {
            target.gameObject.SetActive(true);
        }
    }
}
