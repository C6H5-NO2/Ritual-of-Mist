using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ThisGame.Adventure {
    public class DefaultDragUI : Utils.DragableUI {
        public Transform DropSlot { private get; set; }

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private Transform originParent;
        private Vector2 originSize;


        public override void OnBeginDrag(PointerEventData eventData) {
            base.OnBeginDrag(eventData);
            canvasGroup.blocksRaycasts = false;
            DropSlot = null;
        }


        public override void OnEndDrag(PointerEventData eventData) {
            base.OnEndDrag(eventData);

            // todo: placeholder / fit position / etc
            if(DropSlot == null) {
                if(rectTransform.parent != originParent) {
                    rectTransform.SetParent(originParent);
                    rectTransform.sizeDelta = originSize;
                }
            }
            else {
                if(rectTransform.parent != DropSlot) {
                    rectTransform.SetParent(DropSlot);
                }
            }

            canvasGroup.blocksRaycasts = true;
        }


        protected override void Awake() {
            base.Awake();

            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = transform as RectTransform;
            originParent = transform.root;
            originSize = rectTransform.sizeDelta;
        }
    }
}
