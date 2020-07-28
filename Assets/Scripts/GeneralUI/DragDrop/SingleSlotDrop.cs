using ThisGame.Items;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.GeneralUI {
    public class SingleSlotDrop : MonoBehaviour, IDropHandler {
        // one slot one item
        public ItemDescription Item =>
            transform.childCount == 0
                ? null
                : transform.GetChild(0).GetComponent<ItemDescHolder>().Description;


        public void OnDrop(PointerEventData eventData) {
            var go = eventData.pointerDrag;
            if(go.CompareTag("DragItem")) {
                var scrp = go.GetComponent<ItemDrag>();
                scrp.DropSlot = this.transform;
            }
        }
    }
}
