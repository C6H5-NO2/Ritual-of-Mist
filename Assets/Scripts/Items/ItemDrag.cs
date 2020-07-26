using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisGame.GeneralUI {
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemDrag : MonoBehaviour,
                            IBeginDragHandler,
                            IDragHandler,
                            IEndDragHandler {
        public SingleSlotDrop DropSlot { private get; set; }

        private Camera refCam;
        private Vector2 screenWorldOffset;

        private CanvasGroup canvasGroup;
        private Transform initialParent;

        private Vector2 positionBeginDrag;
        private Transform parentBeginDrag;

        private RectTransform rectTransform;
        private Vector2 initialSize;


        public void OnBeginDrag(PointerEventData eventData) {
            positionBeginDrag = rectTransform.anchoredPosition;
            parentBeginDrag = transform.parent;
            transform.SetParent(Utils.InSceneObjRef.Instance.Focus);

            screenWorldOffset = (Vector2)refCam.WorldToScreenPoint(transform.position) - eventData.position;

            canvasGroup.blocksRaycasts = false;
            DropSlot = null;
        }


        public void OnDrag(PointerEventData eventData) {
            transform.position = (Vector2)refCam.ScreenToWorldPoint(eventData.position + screenWorldOffset);
        }


        /// OnEndDrag is called after OnDrop
        public void OnEndDrag(PointerEventData eventData) {
            if(gameObject == null)
                // destroyed by slot
                return;

            if(DropSlot == null) {
                if(parentBeginDrag == initialParent)
                    OnCanvasToCanvas();
                else
                    OnSlotToCanvas();
            }
            else {
                if(parentBeginDrag == initialParent)
                    OnCanvasToSlot();
                else if(parentBeginDrag == DropSlot.transform)
                    OnSlotToSelf();
                else
                    OnSlotToSlot();
            }

            canvasGroup.blocksRaycasts = true;
        }


        private void OnCanvasToCanvas() {
            transform.SetParent(initialParent);
        }


        private void OnSlotToCanvas() {
            transform.SetParent(initialParent);
            rectTransform.sizeDelta = initialSize;
        }


        private void OnSlotToSelf() {
            transform.SetParent(parentBeginDrag);
            rectTransform.anchoredPosition = Vector2.zero;
        }


        private const float ScaleRatio = 0.796f;

        private void OnCanvasToSlot() {
            var slot = (RectTransform)DropSlot.transform;

            if(slot.childCount != 0) {
                var child = slot.GetChild(0).GetComponent<ItemDrag>();
                child.OnSlotToCanvas();
                ((RectTransform)child.transform).anchoredPosition = positionBeginDrag;
            }

            transform.SetParent(slot);
            rectTransform.sizeDelta = slot.sizeDelta * ScaleRatio;
            rectTransform.anchoredPosition = Vector2.zero;
        }


        private void OnSlotToSlot() {
            var slot = (RectTransform)DropSlot.transform;

            if(slot.childCount != 0) {
                var child = (RectTransform)slot.GetChild(0);
                child.SetParent(parentBeginDrag);
                child.sizeDelta = ((RectTransform)parentBeginDrag).sizeDelta * ScaleRatio;
                child.anchoredPosition = Vector2.zero;
            }

            transform.SetParent(slot);
            rectTransform.sizeDelta = slot.sizeDelta * ScaleRatio;
            rectTransform.anchoredPosition = Vector2.zero;
        }


        private void Awake() {
            //refCam = transform.root.GetComponent<Canvas>().worldCamera;
            refCam = Utils.InSceneObjRef.Instance.MainCamera;
            canvasGroup = GetComponent<CanvasGroup>();
            //initialParent = transform.root;
            initialParent = Utils.InSceneObjRef.Instance.OnTable;
            rectTransform = (RectTransform)transform;
            initialSize = rectTransform.sizeDelta;
        }
    }
}
