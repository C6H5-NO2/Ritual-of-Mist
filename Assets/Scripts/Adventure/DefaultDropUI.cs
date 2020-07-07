using UnityEngine;
using UnityEngine.EventSystems;

namespace Adventure {
    public class DefaultDropUI : MonoBehaviour, IDropHandler {
        public void OnDrop(PointerEventData eventData) {
            var dragUI = eventData.pointerDrag.GetComponent<DefaultDragUI>();
            if(dragUI != null)
                dragUI.DropSlot = transform;
        }
    }
}
