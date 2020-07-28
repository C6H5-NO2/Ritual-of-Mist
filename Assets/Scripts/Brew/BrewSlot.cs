using ThisGame.GeneralUI;
using ThisGame.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ThisGame.Brew {
    public class BrewSlot : MonoBehaviour, IDropHandler {
        public Image placeHolder;
        private bool receiveDrop;
        private BrewUI brewUI; // assert parent has BrewUI

        public void LockItem() {
            receiveDrop = false;

            // no item
            if(transform.childCount < 2) {
                placeHolder.gameObject.SetActive(false);
                placeHolder.sprite = null;
                return;
            }

            var holder = transform.GetChild(1).GetComponent<ItemDescHolder>();
            placeHolder.sprite = holder.Description.image;
            placeHolder.gameObject.SetActive(true);
            Destroy(holder.gameObject);
        }

        public void UnlockItem() {
            receiveDrop = true;
            placeHolder.gameObject.SetActive(false);
            placeHolder.sprite = null;
        }

        public void DestroyItem() {
            receiveDrop = true;
            if(transform.childCount > 1)
                Destroy(transform.GetChild(1).gameObject);
        }

        private void OnTransformChildrenChanged() {
            // todo: cache Empty & Item
            brewUI.OnSlotItemChange();
        }

        private void Awake() {
            brewUI = transform.parent.GetComponent<BrewUI>();
            UnlockItem();
        }

        public bool Empty => transform.childCount < 2 ||
                             !transform.GetChild(1).GetComponent<ItemDescHolder>().isActiveAndEnabled;


        public ItemDescription Item
            => transform.childCount < 2 ? null : transform.GetChild(1).GetComponent<ItemDescHolder>().Description;

        public void OnDrop(PointerEventData eventData) {
            if(!receiveDrop)
                return;
            var go = eventData.pointerDrag;
            if(go.CompareTag("DragItem")) {
                var scrp = go.GetComponent<ItemDrag>();
                scrp.DropSlot = this.transform;
            }
        }
    }
}
