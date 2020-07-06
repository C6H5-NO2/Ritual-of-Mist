using UnityEngine;
using UnityEngine.EventSystems;

namespace Adventure {
    public class OnMapClick : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData eventData) {
            if(eventData.button == PointerEventData.InputButton.Left)
                MapManager.Instance.locDescUI.gameObject.SetActive(false);
        }
    }
}
