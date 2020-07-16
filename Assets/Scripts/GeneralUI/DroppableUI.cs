using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.GeneralUI {
    public class DroppableUI : MonoBehaviour, IDropHandler {
        public void OnDrop(PointerEventData eventData) {
            var go = eventData.pointerDrag;
            if(go.CompareTag("DragItem")) {
                var dragscrp = go.GetComponent<DragDropUI>();
                dragscrp.DropSlot = this;
            }
        }

        public virtual void OnAfterDrop(DragDropUI scrp) { }
    }
}
