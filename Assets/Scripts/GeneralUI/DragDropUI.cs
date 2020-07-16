using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.GeneralUI {
    [RequireComponent(typeof(CanvasGroup))]
    public class DragDropUI : DraggableUI {
        public DroppableUI DropSlot { protected get; set; }

        private CanvasGroup canvasGroup;
        private Transform originParent;


        public override void OnBeginDrag(PointerEventData eventData) {
            base.OnBeginDrag(eventData);
            canvasGroup.blocksRaycasts = false;
            DropSlot = null;
        }


        public override void OnEndDrag(PointerEventData eventData) {
            base.OnEndDrag(eventData);

            // todo: placeholder / fit position / etc
            if(DropSlot == null) {
                if(transform.parent != originParent)
                    OnDropOut();
            }
            else {
                if(transform.parent != DropSlot.transform)
                    OnDropToOther();
                // todo: drop to the same slot
            }

            canvasGroup.blocksRaycasts = true;
        }


        protected virtual void OnDropOut() {
            transform.SetParent(originParent);
        }


        protected virtual void OnDropToOther() {
            transform.SetParent(DropSlot.transform);
            DropSlot.OnAfterDrop(this);
        }


        protected override void Awake() {
            base.Awake();
            canvasGroup = GetComponent<CanvasGroup>();
            originParent = transform.root;
        }
    }
}
