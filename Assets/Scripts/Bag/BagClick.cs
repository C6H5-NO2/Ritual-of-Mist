using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.Bag {
    public class BagClick : MonoBehaviour, IPointerClickHandler {
        public BagUI bagUI;

        public void OnPointerClick(PointerEventData eventData) {
            //if(EventSystem.current.IsPointerOverGameObject())
            //    return;
            bagUI.gameObject.SetActive(true);
        }
    }
}
